using Database;
using DataBase;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class ModelProxy : Proxy
    {

        public new static string NAME = typeof(ModelProxy).FullName;

        Dictionary<int, ModelData> datas;

        public ModelProxy() : base(NAME) { }

        public override void OnRegister()
        {
            datas = new Dictionary<int, ModelData>();
        }

        public override void OnRemove()
        {
        }


        public Dictionary<int, ModelData> GetDatas() => datas;


        public ModelData GetData(int id)
        {
            datas.TryGetValue(id, out ModelData data);
            return data;
        }


        public void SetData(ModelData data)
        {
            datas[data.id] = data;
        }

        public void SetData(HeroVO vo)
        {

        }

        public void ClearPlayerPrefs(string key)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteKey(key);

        }
        public void ClearPlayerPrefs(List<string> keys)
        {
            PlayerPrefs.DeleteAll();

        }
    }
}
