using Database;
using DataBase;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class ItemProxy : Proxy
    {

        public new static string NAME = typeof(ItemProxy).FullName;

        Dictionary<int, ItemVO> datas;

        public ItemProxy() : base(NAME) { }

        public override void OnRegister()
        {
            datas = new Dictionary<int, ItemVO>();
        }

        public override void OnRemove()
        {
        }





        public ItemVO GetData(int id)
        {
            datas.TryGetValue(id, out ItemVO data);
            return data;
        }

        public Dictionary<int, ItemVO> Datas() => datas;

        public void SetData(ItemVO data) => datas[data.id] = data;

    }
}
