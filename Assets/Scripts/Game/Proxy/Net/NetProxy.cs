using Google.Protobuf;
using Net;
using Proto;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
        SocketClient socketClient;
        private bool IsConnected = false;
        private bool isInitialized = false;

        public byte[] TempKey = null;
        public byte[] SessionKey = null;

        public AuthVO authVO;
        CharacterProxy characterProxy = null;

        private long _serverTime;
        private float _nowTime;
        private Dictionary<Type, AckHandler> _ackHandlerDic;
        public NetState CurrentNetState = NetState.disconnected;

        static Queue<KeyValuePair<Type, object>> sEvents = new Queue<KeyValuePair<Type, object>>();

        public NetProxy() : base(NAME) { }

        public override void OnRegister()
        {
            base.OnRegister();
            _ackHandlerDic = new Dictionary<Type, AckHandler>();
            characterProxy = Facade.RetrieveProxy(CharacterProxy.NAME) as CharacterProxy;
        }

        public override void OnRemove()
        {
        }

        public void RegisterHandler(Type type, AckHandler handler)
        {
            //one type may have more than one handler
            if (_ackHandlerDic.ContainsKey(type))
            {
                _ackHandlerDic[type] += handler;
            }
            else
            {
                _ackHandlerDic.Add(type, handler);
            }
        }

        public void RemoveHandler(Type type, AckHandler handler)
        {
            if (_ackHandlerDic != null && _ackHandlerDic.ContainsKey(type))
            {
                _ackHandlerDic[type] -= handler;
            }
        }


        public SocketClient SocketClient
        {
            get
            {
                if (socketClient == null)
                {
                    socketClient = new SocketClient();
                }
                return socketClient;
            }
        }

        public bool IsNetConnected() => IsConnected;

        void Init()
        {
            NetDebug.instance.Log("Enter NetManager", NET_DEBUG_MESSAGE_TYPE.NORMAL);
            if (isInitialized)
            {
                NetDebug.instance.Log("NetManager is Initialized and return", NET_DEBUG_MESSAGE_TYPE.NORMAL);
                return;
            }

            _ackHandlerDic = new Dictionary<Type, AckHandler>();

            authVO = new AuthVO();
            //SocketClient.Init();

            //SetServerConfig();
            //SetClientInfo();

            _serverTime = 0;
            _nowTime = Time.realtimeSinceStartup;

            isInitialized = true;
            NetDebug.instance.Log("NetManager initialision is done", NET_DEBUG_MESSAGE_TYPE.NORMAL);

            if (!IsConnected)
                SocketClient.Init();
        }

        public void ConnectSocket()
        {
            if (isInitialized && (!IsConnected))
            {
                SocketClient.Init();
            }
        }


        public void SendAuthMsg()
        {
            IsConnected = true;
            NetDebug.instance.Log("Server Connected", NET_DEBUG_MESSAGE_TYPE.NORMAL);

            AuthProxy authProxy = Facade.RetrieveProxy(AuthProxy.NAME) as AuthProxy;
            if (TempKey != null && SessionKey == null)
                authProxy.SendKeyExchangeReq();
            else
                authProxy.SendAuthReq();
        }

        /// <summary>
        /// 派发协议
        /// </summary>
        /// <param name="protoId"></param>
        /// <param name="buff"></param>
        public void DispatchProto(uint protoId, byte[] buff)
        {
            if (!ProtoDic.ContainProtoId(protoId))
            {
                NetDebug.instance.LogError("Unkonw ProtoId, id = " + protoId);
                return;
            }
            Type protoType = ProtoDic.GetProtoTypeByProtoId(protoId);
            try
            {
                MessageParser messageParser = ProtoDic.GetMessageParser(protoType.TypeHandle);
                object toc = messageParser.ParseFrom(buff);
                sEvents.Enqueue(new KeyValuePair<Type, object>(protoType, toc));
            }
            catch
            {
                NetDebug.instance.LogError("DispatchProto Error:" + protoType.ToString());
            }

        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessage(IMessage obj)
        {
            if (!ProtoDic.ContainProtoType(obj.GetType()))
            {
                NetDebug.instance.LogError("不存协议类型");
                return;
            }
            ByteBuffer buff = new ByteBuffer();
            uint protoId = ProtoDic.GetProtoIdByProtoType(obj.GetType());

            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                obj.WriteTo(ms);
                result = ms.ToArray();
            }

            Int32 lengh = (Int32)(result.Length + 4);
            //Debug.Log("lengh" + lengh + ",protoId" + protoId);
            buff.WriteInt((Int32)lengh);

            if (SessionKey != null)
            {
                ByteBuffer buffTemp = new ByteBuffer();
                buffTemp.WriteInt((Int32)protoId);
                buffTemp.WriteBytes(result);
                byte[] temp = buffTemp.ToBytes();
                result = EncryptHelper.AESEncrypt(temp, SessionKey);
            }
            else
            {
                buff.WriteInt((Int32)protoId);
            }

            buff.WriteBytes(result);
            SocketClient.SendMessage(buff);
        }

    }
}
