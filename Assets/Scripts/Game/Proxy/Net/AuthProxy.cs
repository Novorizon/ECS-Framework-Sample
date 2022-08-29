using UnityEngine;
using Cspb;
using System;
using PureMVC.Patterns.Proxy;
using Net;

namespace Game
{
    public class AuthProxy : Proxy
    {
        public new static string NAME = typeof(AuthProxy).FullName;

        public AuthProxy() : base(NAME) { }
        NetProxy netProxy = null;
        CharacterProxy characterProxy = null;
        MessageProxy messageProxy = null;

        public override void OnRegister()
        {
            base.OnRegister();
            characterProxy = Facade.RetrieveProxy(CharacterProxy.NAME) as CharacterProxy;
            netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;
            messageProxy = Facade.RetrieveProxy(MessageProxy.NAME) as MessageProxy;

            netProxy.RegisterHandler(typeof(AuthAck), HandlerFunc);
            netProxy.RegisterHandler(typeof(GetGsAck), HandleGsAck);
            netProxy.RegisterHandler(typeof(KeyExchageAck), HandleKeyAck);
        }

        public override void OnRemove()
        {
        }


        private void HandleKeyAck(object data)
        {
            if (data is KeyExchageAck _keyAck)
            {
                byte[] sessionKey = _keyAck.Key.ToByteArray();
                netProxy.SessionKey = EncryptHelper.AESDecrypt(sessionKey, netProxy.TempKey);
            }
        }

        private void HandleGsAck(object data)
        {
            if (data is GetGsAck _gsack)
            {
                //netProxy.authVO.servers = netProxy.authVO.SetServerList(_gsack.Servers);

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
            }
        }

        protected void HandlerFunc(object data)
        {
            AuthAck ack = data as AuthAck;
            NetDebug.instance.Log("AuthMsg: " + ack.ToString(), NET_DEBUG_MESSAGE_TYPE.NORMAL);
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
            KeyExchageReq req = new KeyExchageReq
            {

                Key = Google.Protobuf.ByteString.CopyFrom(EncryptHelper.RSAEncrypt(netProxy.TempKey))
            };
            messageProxy.SendMessage(req);
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
            //try
            //{
            //    AuthReq req = new AuthReq
            //    {
            //        CInfo = GameClientInfo.NetClientInfo,
            //        Type = "anonymous",
            //        Passport = GameClientInfo.NetClientInfo.Udid,//"a1_anonymous_1",
            //        Password = "123456"
            //    };
            //    SendTos(req);
            //}
            //catch (Exception ex)
            //{
            //    NetDebug.instance.LogError("SendAuthReq Error: " + ex.Message.ToString());
            //}


        }

        public void SendCreateCharacterReq(string access_token, long account_id, int server_id)
        {
            CharCreateReq req = new CharCreateReq
            {
                AccessToken = access_token,
                AccountId = account_id,
                ServerId = server_id,
                Name = "PC_" + (DateTime.Now.ToFileTime() % 10000000).ToString(),
                //CInfo = GameClientInfo.NetClientInfo
            };

            messageProxy.SendMessage(req);

            //netProxy.LastCharInfo = netProxy.CurrentCharInfo.Clone();
            //netProxy.CurrentCharInfo.SetServerId(server_id);
            //netProxy.CurrentCharInfo.SetLogined(false);
            //netProxy.CurrentAuthInfo.udid = GameClientInfo.Udid;
            //netProxy.CurrentCharInfo.serverId = server_id;
            //netProxy.CurrentCharInfo.charLogined = false;           
            NetDebug.instance.LogWarning("send create char!!!!!");
        }
        public void SendCharLoginReq(string access_token, long account_id, int server_id, long player_id)
        {

            CharLoginReq req = new CharLoginReq
            {
                AccessToken = access_token,
                AccountId = account_id,
                ServerId = server_id,
                //Name = GameClientInfo.NetClientInfo.Udid,
                //CInfo = GameClientInfo.NetClientInfo,
                PlayerId = player_id
            };

            messageProxy.SendMessage(req);

            NetDebug.instance.LogWarning("send login char!!!!!");
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
                NetDebug.instance.LogWarning("PN token length is 0, not sent!!");
                return;
            }

            PNRegisterReq req = new PNRegisterReq
            {
                PushToken = _token,
                PushType = _type
            };
            NetDebug.instance.LogWarning("Send PN token: " + req.PushToken.ToString());

            messageProxy.SendMessage(req);
        }

    }
}