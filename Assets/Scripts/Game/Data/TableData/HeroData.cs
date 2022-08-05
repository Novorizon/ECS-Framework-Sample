using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace DataBase
{
    public class HeroData : TableData
    {
        public int id;
        public int name;
        public int description { get; set; }
        public int level;
        public int type;
        private bool Updated;

        public HeroData()
        {
            Updated = false;
        }

        public void UpdateHeroToLevel()
        {
            if (!Updated) //this must not be called more than once, otherwise something is wrong
            {
                Updated = true;
            }
        }

        public HeroData Clone()
        {
            HeroData newClone = new HeroData();
            //deep copy
            newClone.id = id;
            newClone.name = name;
            newClone.level = level;
            newClone.type = type;
            return newClone;
        }
    }
}