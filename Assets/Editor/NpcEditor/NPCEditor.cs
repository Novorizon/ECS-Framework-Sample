using UnityEditor;
using Sirenix.OdinInspector.Editor;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor.SceneManagement;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using PureMVC.Patterns.Facade;
using Game;
using DataBase;
using System.Reflection;
using PureMVC.Interfaces;
using System.Linq.Expressions;
using System.Diagnostics;
using Mono.Data.Sqlite;
using Database;

namespace Editor
{

    public partial class NPCEditor
    {
        static string dbName = "Test.db";
        string path = "URI=file:" + Application.streamingAssetsPath + "/" + dbName;
        SQLiteHelper db;

        TableProxy tableProxy;
        NpcProxy npcProxy;
        ModelProxy modelProxy;

        ModelTableAccess access;


        [EditorMode] bool IsProperty = true;
        [EditorMode] bool IsModel = false;
        [EditorMode] bool IsSettings = false;

        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            Facade.SendNotification(notificationName, body, type);
        }

        protected IFacade Facade
        {
            get
            {
                return PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());
            }
        }



        protected int GetInt32(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return 0;

            return reader.GetInt32(reader.GetOrdinal(field));
        }

        protected long GetInt64(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return 0;

            return reader.GetInt64(reader.GetOrdinal(field));
        }

        protected float GetFloat(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return 0;

            return reader.GetFloat(reader.GetOrdinal(field));
        }

        protected string GetString(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return null;

            return reader.GetString(reader.GetOrdinal(field));
        }

        EditorMode mode = EditorMode.Settings;

        void UpdateMode()
        {
            Type type = typeof(NPCEditor);
            FieldInfo[] Infos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            string Name = mode switch
            {
                EditorMode.Property => GetVariableName(() => IsProperty),
                EditorMode.Model => GetVariableName(() => IsModel),
                EditorMode.Settings => GetVariableName(() => IsSettings),
                _ => IsProperty.ToString(),
            };

            for (int i = 0; i < Infos.Length; i++)
            {
                EditorModeAttribute a = Attribute.GetCustomAttribute(Infos[i], typeof(EditorModeAttribute)) as EditorModeAttribute;
                if (a != null)
                {
                    Infos[i].SetValue(this, false);
                    if (Name == Infos[i].Name)
                    {
                        Infos[i].SetValue(this, true);
                    }
                }
            }

        }

        private void InitLIst()
        {
            if (!updated)
            {
                datas = new List<NPCProperty>();

                if (db == null)
                {

                    return;
                }

                db = new SQLiteHelper(path);

                SqliteDataReader reader = db.ReadFullTable("NPC");
                if (reader == null)
                {

                    return;
                }

                List<NPCProperty> list = new List<NPCProperty>();
                while (reader.Read())
                {
                    NPCData npc = new NPCData();
                    NPCProperty data = new NPCProperty();

                    npc.id= data.id = GetInt32(reader, "id");
                    data.name = GetString(reader, "name");
                    data.description = GetString(reader, "description");
                    data.type = (NPCType)GetInt32(reader, "type");
                    int modelId = GetInt32(reader, "modelId");

                    ModelData model = (ModelData)access.GetData(modelId);
                    GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(model.path);
                    data.model = go;
                    datas.Add(data);

                    //modelProxy.GetDatas().Values
                    //npcs.Add()
                }

                reader.Close();
                updated = true;
            }
            mode = EditorMode.Settings;
            UpdateMode();

        }

        private bool ValidateName(string name)
        {
            if (name == null)
                return false;
            string[] s = name.Split('_');
            return (s.Length == 2 && IsNumber(s[0]));
        }
        private int SplitName(ref string name)
        {
            string[] s = name.Split('_');
            if (s.Length == 2 && IsNumber(s[0]))
            {
                int Id = int.Parse(s[0]);
                name = s[1];
                return Id;
            }
            return -1;
        }

        private bool ValidateMapChanged()
        {

            string sceneName = SceneManager.GetActiveScene().name;
            return MapName != MapNameLast || MapName != sceneName;
        }


        static bool IsNumber(string input)
        {
            Regex reg = new Regex("^[1-9]\\d*$");
            return reg.IsMatch(input);
        }
        string GetVariableName<T>(Expression<Func<T>> expr)
        {
            var body = (MemberExpression)expr.Body;

            return body.Member.Name;
        }
    }
}