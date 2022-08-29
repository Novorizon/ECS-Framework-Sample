using Net;
using PureMVC.Patterns.Proxy;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace Game
{
    public class CharacterProxy : Proxy
    {

        public new static string NAME = typeof(CharacterProxy).FullName;

        public CharacterVO character = new CharacterVO();
        public CharacterVO LastCharacter = null;

        public CharacterProxy() : base(NAME) { }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override void OnRemove()
        {
        }


        public void SetServerId(int sid)
        {
            character.serverId = sid;
        }
        public void SetPlayerId(long pid)
        {
            character.playerId = pid;
        }
        public void SetSsid(long ssid)
        {
            character.sessionId = ssid;
        }
        public void SetLogined(bool login)
        {
            character.charLogined = login;
        }
        public long GetPlayerId()
        {
            return character.playerId;
        }
        public int GetServerId()
        {
            return character.serverId;
        }

        public bool IsLogined()
        {
            return character.charLogined;
        }

    }
}
