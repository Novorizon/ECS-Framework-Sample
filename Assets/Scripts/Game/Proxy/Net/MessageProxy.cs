using System;
using Google.Protobuf;
using Cspb;
using PureMVC.Patterns.Proxy;
using Net;

namespace Game
{
    public enum MSG_FREQUENCY_TYPE
    {
        IMMEDIATE,
        TIME_CONTROLED,
        NONE,
    }

    public class MessageProxy : Proxy
    {
        public new static string NAME = typeof(MessageProxy).FullName;

        public DateTime m_LastSendTime = DateTime.MinValue;
        private MSG_FREQUENCY_TYPE m_frequency = MSG_FREQUENCY_TYPE.IMMEDIATE;
        private static int CONTROL_TIME = 30 * 1000; // millisecond

        NetProxy netProxy = null;
        CharacterProxy characterProxy = null;

        public MessageProxy() : base(NAME) { }

        public override void OnRegister()
        {
            base.OnRegister();
            netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;
            characterProxy = Facade.RetrieveProxy(CharacterProxy.NAME) as CharacterProxy;
        }

        public override void OnRemove()
        {
        }


        private bool IsControlSendTimeOk()
        {
            long spend = DateTime.Now.Millisecond - m_LastSendTime.Millisecond;
            if (spend > CONTROL_TIME)
            {
                return true;
            }
            return false;
        }

        public void SendMessage(IMessage obj, MSG_FREQUENCY_TYPE _fre = MSG_FREQUENCY_TYPE.IMMEDIATE)
        {
            bool _CanSendGameReq = true;
            if (!characterProxy.IsLogined())
            {
                _CanSendGameReq = false;
                if (obj is AuthReq || obj is HeartBeatReq || obj is UdidReq || obj is CharCreateReq || obj is CharLoginReq || obj is GetGsReq || obj is KeyExchageReq)
                    _CanSendGameReq = true;
            }
            else
            {
                if (!netProxy.IsNetConnected())
                {
                    _CanSendGameReq = false;
                    NetDebug.instance.LogWarning("Socket is not connect, not sending req!!!");
                }
            }

            if (_CanSendGameReq)
            {
                if (_fre == MSG_FREQUENCY_TYPE.TIME_CONTROLED || m_frequency == MSG_FREQUENCY_TYPE.TIME_CONTROLED)
                {
                    _CanSendGameReq = IsControlSendTimeOk();
                }
            }
            if (_CanSendGameReq)
            {
                netProxy.SendMessage(obj);
                m_LastSendTime = DateTime.Now;
                if (obj is HeartBeatReq)
                {
                    NetDebug.instance.Log("Send " + obj.GetType().ToString(), NET_DEBUG_MESSAGE_TYPE.HEART);
                }
                else
                {
                    NetDebug.instance.Log("Send " + obj.GetType().ToString(), NET_DEBUG_MESSAGE_TYPE.SEND_REQ);
                }
            }
            else
            {
                NetDebug.instance.Log("Character is not logged in, Req " + obj.GetType().ToString() + " is not Sent", NET_DEBUG_MESSAGE_TYPE.SEND_REQ);
            }


        }
    }
}