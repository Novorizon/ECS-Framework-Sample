using System;

namespace Net
{
    public enum NET_DEBUG_MESSAGE_TYPE
    {
        NORMAL,
        SEND_REQ,
        HANDLE_ACK,
        ERROR,
        HEART
    }

    public class NetDebug //: SysUtil.Singleton<NetDebug>
    {




        private static NetDebug _instance;
        public static NetDebug instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NetDebug();
                return _instance;
            }

        }
//#if ENABLE_DEBUG
#if GOLD_BUILD
        public bool _showNetDebugMsg = false;
#else
        public bool _showNetDebugMsg = true;
#endif
//#endif

        public int _debugMsgType = 0x0F;
        //static string debugFilename = Path.Combine(UtilIO.GetPlatformSaveDataDir(), "NetDebugMsg");
        //static FileStream debugFileStream = new FileStream(debugFilename, FileMode.Append);
        //static StreamWriter debugStreamWriter = new StreamWriter(debugFileStream);
        public void Log(string msg, NET_DEBUG_MESSAGE_TYPE type = NET_DEBUG_MESSAGE_TYPE.NORMAL)
        {
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff ") + msg;
            if (_showNetDebugMsg)
            {
                if(((_debugMsgType>>(int)type)&1) == 1)
                    UnityEngine.Debug.Log("Net Debug Msg ==============> : " + msg);
            }
            //if (_saveNetDebugMsg)
            {
                //RecordNetDebug(msg);
            }
        }
        public void LogWarning(string msg)
        {
            if (_showNetDebugMsg)
            {
                UnityEngine.Debug.LogWarning("Net Debug Msg ==============> : " + msg);
            }
        }
        public void LogError(string msg)
        {
            //if (_showNetDebugMsg)
            {
                UnityEngine.Debug.LogError("Net Debug Msg ==============> : " + msg);
            }
        }

        public void RecordNetDebug(string msg)
        {
            //debugStreamWriter.WriteLineAsync(msg);        
        }


    }

}
