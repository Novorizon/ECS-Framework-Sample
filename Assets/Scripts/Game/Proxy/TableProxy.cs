using Database;
using DataBase;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class TableProxy : Proxy
    {

        public new static string NAME = typeof(TableProxy).FullName;

        private Dictionary<Type, TableAccess> accessors = new Dictionary<Type, TableAccess>();
        private Dictionary<Type, Type> accessorTypes;

        string path;

        public TableProxy() : base(NAME) { }

        public override void OnRegister()
        {
            path = "";
        }

        public override void OnRemove()
        {
        }

        private void RegisterTable<T>(AccessType type = AccessType.Immediately) where T : TableAccess, new()
        {
            if (accessors == null || accessorTypes == null || accessors.ContainsKey(typeof(T)))
                return;

            T accessor = new T();
            accessor.Type = type;

            accessors.Add(typeof(T), accessor);

            if (!accessorTypes.ContainsKey(accessor.DataType))
                accessorTypes.Add(accessor.DataType, typeof(T));
        }

        public bool Load()
        {
            SQLiteHelper db = new SQLiteHelper(path);

            if (accessors == null || db == null)
                return false;

            foreach (TableAccess accessor in accessors.Values)
            {
                if (accessor == null || accessor.Loaded)
                    continue;

                if (accessor.Type != AccessType.Immediately)
                    continue;

                if (!accessor.Load(db))
                {
                    Debug.Log("Failed to load table:" + accessor.Name);
                }

            }
            db.Close();

            return true;
        }

        public bool Load<T>() where T : TableAccess, new()
        {

            if (accessors == null)
                return false;

            if (!accessors.ContainsKey(typeof(T)))
                return false;

            TableAccess accessor = (T)accessors[typeof(T)];
            if (accessor == null || accessor.Loaded)
                return false;

            SQLiteHelper db = new SQLiteHelper(path);
            if (db == null)
                return false;

            if (!accessor.Load(db))
            {
                Debug.Log("Failed to load table:" + accessor.Name);
            }

            db.Close();

            return true;
        }

        public T GetAccess<T>() where T : TableAccess, new()
        {
            if (!accessors.ContainsKey(typeof(T)))
                return null;

            return (T)accessors[typeof(T)];
        }

        public T GetData<T>(int id) where T : TableData
        {
            if (accessorTypes == null || !accessorTypes.ContainsKey(typeof(T)))
                return null;

            Type type = accessorTypes[typeof(T)];

            if (accessors == null || !accessors.ContainsKey(type))
                return null;

            TableAccess accessor = accessors[type];

            if (accessor != null)
            {
                return (T)accessor.GetData(id);
            }

            return null;
        }


    }
}
