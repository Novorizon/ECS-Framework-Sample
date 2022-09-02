using System;
using PureMVC.Patterns.Proxy;
using Game.Protobuffer;
using UnityEngine;

namespace Game
{
    public class AuthProxy : Proxy
    {
        public new static string NAME = typeof(AuthProxy).FullName;

        public AuthProxy() : base(NAME) { }
        NetProxy netProxy = null;
        CharacterProxy characterProxy = null;
        MessageProxy messageProxy = null;
        HandlerProxy handlerProxy = null;

        public override void OnRegister()
        {
            base.OnRegister();
            characterProxy = Facade.RetrieveProxy(CharacterProxy.NAME) as CharacterProxy;
            netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;
            messageProxy = Facade.RetrieveProxy(MessageProxy.NAME) as MessageProxy;
            handlerProxy = Facade.RetrieveProxy(HandlerProxy.NAME) as HandlerProxy;

            handlerProxy.RegisterHandler(typeof(S2C_LoginAck), HandleLoginAck);
        }

        public override void OnRemove()
        {
        }

        public void Login()
        {
            C2S_Login req = new C2S_Login();
            messageProxy.SendMessage(req);
        }


        private void HandleLoginAck(object data)
        {
            //if (data is KeyExchageAck _keyAck)
            //{
            //    byte[] sessionKey = _keyAck.Key.ToByteArray();
            //    netProxy.SessionKey = EncryptHelper.AESDecrypt(sessionKey, netProxy.TempKey);
            //}
        }


        protected void HandlerFunc(object data)
        {
            //AuthAck ack = data as AuthAck;
            //NetDebug.instance.Log("AuthMsg: " + ack.ToString(), NET_DEBUG_MESSAGE_TYPE.NORMAL);
            //netProxy.LastAuthInfo = netProxy.CurrentAuthInfo.Clone();
            //netProxy.CurrentAuthInfo.SetInfo(ack);

            //if (SceneInfoMgr.GetInstance().IsBigMapActive())
            //{                
            //    if(netProxy.CurrentAuthInfo.IsLastServerExisted())
            //        MsgAuth.Instance.SendCharLoginReq(ack.AccessToken, ack.AccountId, netProxy.CurrentCharInfo.GetServerId(), netProxy.CurrentCharInfo.GetPlayerId());
            //    else
            //    {
            //    netProxy.CurrentNetState = NetState.lost_server;
            //    }
            //    return;
            //}
            //LoginMenu _loginMenu = (LoginMenu)UIManager.GetInstance().GetUIFormByName("LoginMenu");
            //if (_loginMenu != null && _loginMenu.isActiveAndEnabled)
            //{
            //    UIGameLoginArgs args = new UIGameLoginArgs()
            //    {
            //        loginstep = "info",
            //        characterlist = netProxy.CurrentAuthInfo.characters,
            //        serverlist = netProxy.CurrentAuthInfo.servers
            //    };
            //    UIEventManager.GetInstance().DispatchEvent(UIEventManager.UIEventType.GameLogin, args);

            //}
            //DebugMenu debugMenu = (DebugMenu)UIManager.GetInstance().GetUIFormByName("DebugMenu");
            //if ( debugMenu!= null)
            //{
            //    NetLogin loginPage = debugMenu.GetComponent<NetLogin>();
            //    GameObject loginPageOjb = GameObject.Find("NetLoginPage");
            //    if(loginPageOjb != null)
            //    {
            //        //if (!loginPage._loginButton.enabled)
            //        {
            //            loginPage._loginButton.gameObject.SetActive(true);
            //            loginPage._loginButton.enabled = true;
            //            loginPage.SetUpdate();
            //            loginPage.CheckUpdate();
            //        }
            //        if (ack.Characters.Count == 0)
            //        {
            //            loginPage.EnableCreateNewChar();
            //        }
            //        else
            //        {
            //            foreach (Cspb.Character character in ack.Characters)
            //            {
            //                if (character.ServerId == loginPage.GetServerChoosen() && character.PlayerId == loginPage.GetCharChoosen())
            //                {
            //                    //loginPage.DisableCreateNewChar();
            //                    return;
            //                }
            //            }
            //        }
            //        /*
            //        if (ack.RecommandServer == 0)
            //        {
            //        netProxy.OnDisConnect();
            //        }
            //        else
            //        {
            //            loginPage.EnableCreateNewChar();
            //        }
            //        */

            //        return;
            //    }

            //}

            return;
        }
        public void SendKeyExchangeReq()
        {
            //KeyExchageReq req = new KeyExchageReq
            //{
            //    Key = Google.Protobuf.ByteString.CopyFrom(EncryptHelper.RSAEncrypt(netProxy.TempKey))
            //};
            //messageProxy.SendMessage(req);
        }

        //public void SendGsReq()
        //{
        //    GetGsReq req = new GetGsReq
        //    {
        //        RegionId = ""
        //    };
        //    SendTos(req);
        //}

        public void SendAuthReq()
        {
            try
            {
                C2S_Login req = new C2S_Login
                {
                    PlayerId = 1,
                };

                if (messageProxy == null)
                    messageProxy = Facade.RetrieveProxy(MessageProxy.NAME) as MessageProxy;

                messageProxy.SendMessage(req);
            }
            catch (Exception ex)
            {
                Debug.LogError("SendAuthReq Error: " + ex.Message.ToString());
            }


        }

        public void SendCreateCharacterReq(string access_token, long account_id, int server_id)
        {
            //CharCreateReq req = new CharCreateReq
            //{
            //    AccessToken = access_token,
            //    AccountId = account_id,
            //    ServerId = server_id,
            //    Name = "PC_" + (DateTime.Now.ToFileTime() % 10000000).ToString(),
            //    //CInfo = GameClientInfo.NetClientInfo
            //};

            //messageProxy.SendMessage(req);

            //netProxy.LastCharInfo = netProxy.CurrentCharInfo.Clone();
            //netProxy.CurrentCharInfo.SetServerId(server_id);
            //netProxy.CurrentCharInfo.SetLogined(false);
            //netProxy.CurrentAuthInfo.udid = GameClientInfo.Udid;
            //netProxy.CurrentCharInfo.serverId = server_id;
            //netProxy.CurrentCharInfo.charLogined = false;           
            Debug.LogWarning("send create char!!!!!");
        }
        public void SendCharLoginReq(string access_token, long account_id, int server_id, long player_id)
        {

            //CharLoginReq req = new CharLoginReq
            //{
            //    AccessToken = access_token,
            //    AccountId = account_id,
            //    ServerId = server_id,
            //    //Name = GameClientInfo.NetClientInfo.Udid,
            //    //CInfo = GameClientInfo.NetClientInfo,
            //    PlayerId = player_id
            //};

            //messageProxy.SendMessage(req);

            Debug.LogWarning("send login char!!!!!");
            //netProxy.LastCharInfo = netProxy.CurrentCharInfo.Clone();
            //netProxy.CurrentCharInfo.SetServerId(server_id);
            //netProxy.CurrentCharInfo.SetLogined(false);
            //netProxy.CurrentCharInfo.SetPlayerId(player_id);
            //netProxy.CurrentAuthInfo.accessToken = access_token;
            //netProxy.CurrentAuthInfo.udid = GameClientInfo.Udid;
            //netProxy.CurrentCharInfo = server_id;
            //netProxy.CurrentCharInfo.charLogined = false;
            //netProxy.CurrentAuthInfo.charLogined = false;
        }
        public void SendPNRegisterReq(string _token, string _type)
        {
            if (_token.Length <= 0)
            {
                Debug.LogWarning("PN token length is 0, not sent!!");
                return;
            }

            //PNRegisterReq req = new PNRegisterReq
            //{
            //    PushToken = _token,
            //    PushType = _type
            //};
            //NetDebug.instance.LogWarning("Send PN token: " + req.PushToken.ToString());

            //messageProxy.SendMessage(req);
        }

    }
}