using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Database;

namespace DataBase
{
    [TableAccess]
    public class HeroTableAccess : TableAccess
    {
        Dictionary<int, TableData> datas;
        public override Type DataType => typeof(HeroData);

        public Dictionary<int, TableData> GetDatas() => datas;
  

        public HeroTableAccess()
        {
            Name = "Hero";
            Loaded = false;
            datas = new Dictionary<int, TableData>();
        }


        public override bool Load(SQLiteHelper db)
        {
            if (db == null || string.IsNullOrEmpty(Name))
                return false;

            SqliteDataReader reader = db.ReadFullTable(Name);
            if (reader == null)
                return false;

            HeroData data = null;

            while (reader.Read())
            {
                data = new HeroData();

                data.id = GetInt32(reader, "id");
                data.name = GetString(reader, "name");
                data.description = GetInt32(reader, "description");
                data.type = GetInt32(reader, "type");

                datas.Add(data.id, data);
            }

            reader.Close();
            Loaded = true;

            return true;
        }

        public override TableData GetData(int index)
        {
            if (datas == null || !datas.ContainsKey(index))
                return null;

            return datas[index];
        }



    }

}