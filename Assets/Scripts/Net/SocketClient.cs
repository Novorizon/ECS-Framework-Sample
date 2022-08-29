using System;
using System.IO;
using System.Net.Sockets;
using Net;
using System.Threading;
using Proto;
using Cspb;
using Game;
using PureMVC.Patterns.Facade;
using PureMVC.Interfaces;

public enum DisType
{
    Exception,
    Disconnect,
}

public class SocketClient
{

    NetProxy netProxy = null;
    AuthProxy authProxy = null;

    public int ThreadSleepTime = 5000;
    private TcpClient client = null;
    private NetworkStream outStream = null;
    private MemoryStream memStream;
    private BinaryReader reader;

    private const int MAX_READ = 8192;
    private byte[] byteBuffer = new byte[MAX_READ];
    public long LastConnectTime = -1;
    public int ReConnectTime;
    private int TRY_TIME = 6;
    public static ManualResetEvent manualEvent = null;
    private Thread thread;

    string host = "127.0.0.1";
    int port = 6688;

    public Int32 ConnectTimeout = 30000;

    protected IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new Facade());

    public void SendNotification(string notificationName, object body = null, string type = null)
    {
        Facade.SendNotification(notificationName, body, type);
    }

    public SocketClient()
    {

    }

    public void Init()
    {
        netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;
        authProxy = Facade.RetrieveProxy(AuthProxy.NAME) as AuthProxy;

        memStream = new MemoryStream();
        reader = new BinaryReader(memStream);

        host = "127.0.0.1";
        port = 6688;

        netProxy.CurrentNetState = NetState.connected;

        ReConnectTime = TRY_TIME;

        manualEvent = new ManualResetEvent(true);

        InitThread();
        NetDebug.instance.LogWarning("Socket Init: host:" + host + " port: " + port);
    }

    public void ReSetTryTime()
    {
        ReConnectTime = TRY_TIME;
    }
    public void ReSetSocket()
    {
        memStream = new MemoryStream();
        reader = new BinaryReader(memStream);

        host = "127.0.0.1";
        port = 6688;

        manualEvent = new ManualResetEvent(true);
        InitThread();
        NetDebug.instance.LogWarning("Reset Socket!!!!!" + ReConnectTime);
    }
    public void InitThread()
    {
        if (thread != null)
        {
            thread.Abort();
            thread = null;
        }
        thread = new Thread(new ThreadStart(this.ConnectServer))
        {
            IsBackground = true
        };
        thread.Start();
        NetDebug.instance.LogWarning("InitThread!!!");
    }

    public bool Check()
    {
        if (ReConnectTime <= 0)// || mySocketState == SocketState.connected)
        {
            NetDebug.instance.LogWarning("Check retry stop event:" + ReConnectTime.ToString() + " socketstate:" + netProxy.CurrentNetState.ToString());
            return false;
        }
        NetDebug.instance.LogWarning("Check retry continue:" + ReConnectTime.ToString());
        return true;
    }
    void ConnectServer()
    {

        while (true)
        {
            NetDebug.instance.LogWarning("Start !!!!!!!!!!!!!!!!!!!!!!" + ReConnectTime);
            manualEvent.WaitOne();
            LastConnectTime = TimeUtil.TickToMilliSec(DateTime.Now.Ticks);
            try
            {
                client = null;
                client = new TcpClient();
                client.SendTimeout = 5000;
                client.ReceiveTimeout = 10 * 1000;
                client.NoDelay = true;
                NetDebug.instance.LogWarning("BeginConnect to:" + host + ":" + port.ToString());
                client.BeginConnect(host, port, new AsyncCallback(ConnectCallback), client);

                netProxy.CurrentNetState = NetState.connecting;

                ReConnectTime--;
                NetDebug.instance.LogWarning("Done !!!!!!!!!!!!!!!!!!!!!!" + ReConnectTime);
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
    private void ConnectCallback(IAsyncResult asyncresult)
    {
        try
        {
            client = asyncresult.AsyncState as TcpClient;
            if (client != null)
            {
                //Debug.Log("1");
                client.EndConnect(asyncresult);
                //Debug.Log("2");
                NetDebug.instance.LogWarning("Socket connected to " +
                client.Client.RemoteEndPoint.ToString());


                netProxy.CurrentNetState = NetState.connect_done;


                ReConnectTime = TRY_TIME;
                StartReceive(client);


                if (netProxy.TempKey != null)
                {
                    authProxy.SendKeyExchangeReq();
                    netProxy.CurrentNetState = NetState.wait_key;
                    SocketClient.manualEvent.Set();
                    netProxy.SessionKey = null;
                }
                else
                {
                    NetDebug.instance.LogWarning("No TempKey send auth");
                    netProxy.SendAuthMsg();
                }


                NetDebug.instance.LogWarning("ConnectCallback success and StartReceive");
            }
            else
            {
                throw new TimeoutException("Socket Client Error");
            }
        }
        catch (Exception ex)
        {
            OnError("ConnectCallback() " + ex.Message.ToString());
        }
        finally
        {
        }
    }

    void OnConnect(IAsyncResult asr)
    {
        if (client.Connected)
        {
            outStream = client.GetStream();
            client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
        }
    }
    private void StartReceive(TcpClient _client)
    {
        try
        {
            Array.Clear(byteBuffer, 0, byteBuffer.Length);
            client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), _client);
        }
        catch (Exception ex)
        {
            OnError("StartReceive(): " + ex.Message.ToString());
        }
    }

    void WriteMessage(byte[] message)
    {
        MemoryStream ms = null;
        BinaryWriter writer = null;
        try
        {

            using (ms = new MemoryStream())
            {
                ms.Position = 0;
                writer = new BinaryWriter(ms);
                writer.Write(message);
                writer.Flush();
                outStream = client.GetStream();
                if (client != null && client.Connected && outStream != null)
                {
                    byte[] payload = ms.ToArray();
                    outStream.BeginWrite(payload, 0, payload.Length, new AsyncCallback(OnWrite), null);
                }
                else
                {
                    OnError("WriteMessage Error");
                }
            }
        }
        catch (Exception ex)
        {
            OnError("WriteMessage Error " + ex.Message.ToString());
        }
        finally
        {
            if (writer != null) writer.Dispose();
            if (ms != null) ms.Dispose();
        }

    }

    void OnRead(IAsyncResult asr)
    {
        int bytesRead = 0;
        try
        {
            client = asr.AsyncState as TcpClient;
            if (client != null)
            {
                lock (client.GetStream())
                {
                    bytesRead = client.GetStream().EndRead(asr);
                }
                if (bytesRead < 1)
                {
                    OnError("OnRead() " + "bytesRead < 1");
                    NetDebug.instance.LogError(" bytesRead < 1");
                    return;
                }
                OnReceive(byteBuffer, bytesRead);
                lock (client.GetStream())
                {
                    StartReceive(client);
                }
            }
            else
            {
                throw new TimeoutException("Socket Client Error");
            }

        }
        catch (Exception ex)
        {
            OnError("OnRead(): " + ex.Message.ToString());
        }
    }
    void OnError(string msg)
    {
        //if (mySocketState != SocketState.none)// && mySocketState != SocketState.reconnecting)
        netProxy.CurrentNetState = NetState.disconnected;
        NetDebug.instance.LogError("SocketClient OnError: " + msg);
        ////Close();
        //if(!threadState.isRuning()&&!netProxy.isDestroyed)
        //{
        //    InitThread();
        //    StartThread();                        
        //}        
    }


    /// <summary>
    /// 打印字节
    /// </summary>
    /// <param name="bytes"></param>
    void PrintBytes()
    {
        string returnStr = string.Empty;
        for (int i = 0; i < byteBuffer.Length; i++)
        {
            returnStr += byteBuffer[i].ToString("X2");
        }
        NetDebug.instance.LogError(returnStr);
    }

    /// <summary>
    /// 向链接写入数据流
    /// </summary>
    void OnWrite(IAsyncResult r)
    {
        try
        {
            outStream.EndWrite(r);
        }
        catch (Exception ex)
        {
            NetDebug.instance.LogError("OnWrite--->>>" + ex.Message);
        }
    }

    /// <summary>
    /// 接收到消息
    /// </summary>
    void OnReceive(byte[] bytes, int length)
    {
        memStream.Seek(0, SeekOrigin.End);
        memStream.Write(bytes, 0, length);
        //Reset to beginning
        memStream.Seek(0, SeekOrigin.Begin);
        while (RemainingBytes() > 4)
        {
            Int32 messageLen = reader.ReadInt32();
            if (RemainingBytes() >= messageLen)
            {
                MemoryStream ms = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(ms);
                writer.Write(reader.ReadBytes(messageLen));
                ms.Seek(0, SeekOrigin.Begin);
                OnReceivedMessage(ms);
            }
            else
            {

                memStream.Position = memStream.Position - 4;
                break;
            }
        }
        byte[] leftover = reader.ReadBytes((int)RemainingBytes());
        memStream.SetLength(0);
        memStream.Write(leftover, 0, leftover.Length);
    }

    /// <summary>
    /// 剩余的字节
    /// </summary>
    private long RemainingBytes()
    {
        return memStream.Length - memStream.Position;
    }

    /// <summary>
    /// 接收到消息
    /// </summary>
    /// <param name="ms"></param>
    void OnReceivedMessage(MemoryStream ms)
    {
        try
        {
            BinaryReader r = new BinaryReader(ms);
            byte[] message = r.ReadBytes((int)(ms.Length - ms.Position));
            ByteBuffer buffer = new ByteBuffer(message);
            int mainId = buffer.ReadInt();
            int pbDataLen = message.Length - 4;
            byte[] pbData = buffer.ReadBytes(pbDataLen);
            Type protoType = ProtoDic.GetProtoTypeByProtoId((uint)mainId);
            if (netProxy.SessionKey != null && protoType != typeof(KeyExchageReq))
            {
                pbData = EncryptHelper.AESDecrypt(pbData, netProxy.SessionKey);
            }
            netProxy.DispatchProto((uint)mainId, pbData);
        }
        catch (Exception ex)
        {
            OnError("OnReceivedMessage(): " + ex.Message.ToString());
        }
    }

    /// <summary>
    /// 会话发送
    /// </summary>
    void SessionSend(byte[] bytes)
    {
        WriteMessage(bytes);
    }

    /// <summary>
    /// 关闭链接
    /// </summary>
    public void Close()
    {
        NetDebug.instance.LogError("SocketClient close()");
        if (client != null)
        {
            if (client.Connected) client.Close();
            client = null;
            NetDebug.instance.LogError("SocketClient close done");
        }
    }

    /// <summary>
    /// 发送连接请求
    /// </summary>
    public void SendConnect()
    {
        // ConnectServer(GameClientInfo.SocketAddress, GameClientInfo.SocketPort);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public void SendMessage(ByteBuffer buffer)
    {
        SessionSend(buffer.ToBytes());
        buffer.Close();
    }

    public void OnRemove()
    {
        this.Close();
        reader.Close();
        memStream.Close();
        if (manualEvent != null)
        {
            manualEvent.Dispose();
            manualEvent.Close();
        }

        NetDebug.instance.LogWarning("On Socket remove all!!!");
        if (thread != null)
        {
            thread.Abort();
            thread = null;
        }

    }
}

