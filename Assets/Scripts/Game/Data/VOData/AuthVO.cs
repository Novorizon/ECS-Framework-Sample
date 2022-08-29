using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using UnityEngine;
using Cspb;

namespace Game
{
    public class AuthVO : ICloneable
    {
        public string error = "";
        public List<CharacterVO> characters = new List<CharacterVO>();
        public List<Server> servers = new List<Server>();
        public long accountId = 0;
        public string accessToken = "";
        public long tokenExpire = 0;
        public int recommandServer = 0;
        public string type = "";
        public int clientState = 0;

        public string udid = "";
        public string buildType = "alpha";
        public string revision = "0";

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public AuthVO Clone()
        {
            return (AuthVO)this.MemberwiseClone();
        }
        //public bool IsSameAuth()
        //{
        //    if (NetManager.instance.LastAuthInfo != null)
        //    {
        //        if (accountId == NetManager.instance.LastAuthInfo.accountId)
        //            return true;
        //    }
        //    return false;
        //}
        //public bool IsLastServerExisted()
        //{
        //    if (NetManager.instance.CurrentCharInfo != null && NetManager.instance.CurrentCharInfo.IsLogined())
        //    {
        //        int lastId = NetManager.instance.CurrentCharInfo.GetServerId();
        //        foreach (Server svr in servers)
        //        {
        //            if (svr.ServerId == lastId)
        //                return true;
        //        }
        //    }
        //    return false;
        //}
        //public bool IsTokenOk(string accesstoken)
        //{
        //    if (error == "" && accessToken != "")
        //    {
        //        if (tokenExpire < NetManager.instance.ServerTime)
        //            return true;
        //    }
        //    return false;
        //}
        //public void SetInfo(AuthAck _ack)
        //{
        //    error = _ack.Error;
        //    accessToken = _ack.AccessToken;
        //    characters = SetCharacters(_ack.Characters);
        //    servers = SetServerList(_ack.Servers);
        //    accountId = _ack.AccountId;
        //    recommandServer = _ack.RecommandServer;
        //    type = _ack.Type;
        //    clientState = _ack.ClientState;
        //    tokenExpire = _ack.TokenExplreAt;
        //    udid = GameClientInfo.Udid;
        //}
        //public List<Character> SetCharacters(RepeatedField<Character> cs)
        //{
        //    List<Character> characters = cs.ToList<Character>();
        //    if (cs.Count == 0)
        //    {
        //        return characters;
        //    }
        //    characters.Sort((left, right) =>
        //    {
        //        if (left.LastLogin < right.LastLogin)
        //            return 1;
        //        else
        //            return -1;
        //    });
        //    return characters;
        //}
        //public List<Server> SetServerList(RepeatedField<Server> cs)
        //{
        //    List<Server> servers = cs.ToList<Server>();
        //    if (cs.Count == 0)
        //    {
        //        return servers;
        //    }
        //    servers.Sort((left, right) =>
        //    {
        //        if (left.ServerId < right.ServerId)
        //            return 1;
        //        else
        //            return -1;
        //    });
        //    return servers;
        //}

        public new string ToString()
        {
            return default;
        }
    }
}