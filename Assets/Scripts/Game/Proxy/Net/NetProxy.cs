using Net;
using PureMVC.Patterns.Proxy;
using System;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;

namespace Game
{
    public enum NetState
    {
        connecting,
        //reconnecting,
        connect_done,
        disconnected,
        //tomanytime,
        connected,
        lost_server,
        wait_key,
    }

    public delegate void AckHandler(object data);
    public class NetProxy : Proxy
    {
        public new static string NAME = typeof(NetProxy).FullName;
        public static TCPClientState Client = null;
        public static ManualResetEvent manualEvent = null;

        public NetState CurrentNetState = NetState.disconnected;

        public long LastConnectTime = -1;
        public int ReConnectTime;

        private string host = "127.0.0.1";
        private int port = 6688;

        private MessageProxy messageProxy = null;
        private AuthProxy authProxy = null;

        private Thread thread;
        private int ThreadSleepTime = 5000;
        private int TRY_TIME = 6;
        private const int MAX_READ = 8192;
        private byte[] byteBuffer = new byte[MAX_READ];

        private bool IsConnected = false;
        private bool isInitialized = false;

        private long _serverTime;
        private float _nowTime;
        public byte[] TempKey = null;
        public byte[] SessionKey = null;

        CharacterProxy characterProxy = null;


        public NetProxy() : base(NAME) { }

        public override void OnRegister()
        {
            base.OnRegister();
            characterProxy = Facade.RetrieveProxy(CharacterProxy.NAME) as CharacterProxy;
            authProxy=Facade.RetrieveProxy(AuthProxy.NAME) as AuthProxy;
        }

        public override void OnRemove()
        {
        }


        public bool IsNetConnected() => IsConnected;

        public void Start()
        {
            Debug.Log("Enter NetManager");
            if (isInitialized)
            {
                Debug.Log("NetManager is Initialized and return");
                return;
            }

            host = "127.0.0.1";
            port = 6688;
            _serverTime = 0;
            _nowTime = Time.realtimeSinceStartup;
            CurrentNetState = NetState.connected;
            manualEvent = new ManualResetEvent(true);
            isInitialized = true;

            if (IsConnected)
                return;
            StartConnectServer();
            Debug.LogWarning("Socket Init: host:" + host + " port: " + port);
        }

        public void StartConnectServer()
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
            thread = new Thread(new ThreadStart(this.Connect))
            {
                IsBackground = true
            };
            thread.Start();
            Debug.LogWarning("InitThread!!!");
        }

        public bool Check()
        {
            if (ReConnectTime <= 0)// || mySocketState == SocketState.connected)
            {
                //Debug.LogWarning("Check retry stop event:" + ReConnectTime.ToString() + " socketstate:" + CurrentNetState.ToString());
                return false;
            }
            //Debug.LogWarning("Check retry continue:" + ReConnectTime.ToString());
            return true;
        }

        void Connect()
        {
            while (!IsConnected)
            {
                Debug.LogWarning("Start !!!!!!!!!!!!!!!!!!!!!!" + ReConnectTime);
                manualEvent.WaitOne();
                LastConnectTime = TimeUtil.TickToMilliSec(DateTime.Now.Ticks);
                try
                {

                    TcpClient tcpClient = new TcpClient();
                    tcpClient.SendTimeout = 5000;
                    tcpClient.ReceiveTimeout = 10 * 1000;
                    tcpClient.NoDelay = true;
                    tcpClient.BeginConnect(host, port, new AsyncCallback(HandleConnected), tcpClient);


                    Debug.LogWarning("BeginConnect to:" + host + ":" + port.ToString());

                    CurrentNetState = NetState.connecting;

                    ReConnectTime--;
                    Debug.LogWarning("Done !!!!!!!!!!!!!!!!!!!!!!" + ReConnectTime);
                }
                catch (Exception e)
                {
                    OnError("ConnectServer(): " + e.Message.ToString());
                }
                finally
                {
                    Thread.Sleep(ThreadSleepTime);
                }
            }
        }

        void ReConnect()
        {
            IsConnected = false;
            Client.Close();
            Client.TcpClient.BeginConnect(host, port, new AsyncCallback(HandleConnected), Client.TcpClient);
        }

        private void HandleConnected(IAsyncResult asyncresult)
        {
            if (IsConnected)
                return;

            try
            {
                TcpClient tcpClient = asyncresult.AsyncState as TcpClient;
                if (tcpClient == null)
                {
                    OnError("Socket Client Error");
                    return;
                }

                IsConnected = true;
                tcpClient.EndConnect(asyncresult);

                byte[] buffer = new byte[MAX_READ];
                Client = new TCPClientState(tcpClient, buffer);

                Debug.LogWarning("Socket connected to " + tcpClient.Client.RemoteEndPoint.ToString());

                CurrentNetState = NetState.connect_done;
                ReConnectTime = TRY_TIME;

                Array.Clear(byteBuffer, 0, byteBuffer.Length);
                Client.NetworkStream.BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(HandleDataReceived), Client);


                //登录验证
                if (TempKey != null)
                {
                    authProxy.SendKeyExchangeReq();
                    CurrentNetState = NetState.wait_key;
                    manualEvent.Set();
                    SessionKey = null;
                }
                else
                {
                    Debug.LogWarning("No TempKey send auth");
                    if(authProxy==null)
                        authProxy = Facade.RetrieveProxy(AuthProxy.NAME) as AuthProxy;

                    authProxy.Login();
                }


                Debug.LogWarning("ConnectCallback success and StartReceive");

            }
            catch (Exception ex)
            {
                Debug.LogError("ConnectCallback Error"+ ex.Message);
            }
            finally
            {
            }
        }


        /// 数据接受回调函数
        private void HandleDataReceived(IAsyncResult ar)
        {
            Client = (TCPClientState)ar.AsyncState;

            int count = 0;
            try
            {
                count = Client.NetworkStream.EndRead(ar);
            }
            catch
            {
                count = 0;
            }

            if (count == 0)
            {
                // connection has been closed
                lock (Client)
                {
                    return;
                }
            }

            messageProxy.ReceiveMessage(count);
            Client.NetworkStream.BeginRead(Client.Buffer, 0, Client.Buffer.Length, HandleDataReceived, Client);

        }





        void OnError(string msg)
        {
            Debug.LogError("SocketClient OnError: " + msg);
        }


    }
}
