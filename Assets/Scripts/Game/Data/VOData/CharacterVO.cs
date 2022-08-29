using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using UnityEngine;

namespace Game
{
    public class CharacterVO : ICloneable
    {

        public int serverId = 0;
        public long playerId = 0;
        public long sessionId = 0;

        public bool charLogined = false;



        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public CharacterInfo Clone()
        {
            return (CharacterInfo)this.MemberwiseClone();
        }
    }
}