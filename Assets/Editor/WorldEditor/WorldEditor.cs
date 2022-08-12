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
using System.Linq;
using Sirenix.OdinInspector;

namespace WorldEditor
{
    public class MySelector : OdinSelector<NPCData>
    {
        private readonly List<NPCData> source;
        private readonly bool supportsMultiSelect;

        public MySelector(List<NPCData> source, bool supportsMultiSelect)
        {
            this.source = source;
            this.supportsMultiSelect = supportsMultiSelect;
        }

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Config.DrawSearchToolbar = true;
            tree.Selection.SupportsMultiSelect = this.supportsMultiSelect;

            tree.Add("Defaults/None", null);
            tree.Add("Defaults/A", new NPCData());
            tree.Add("Defaults/B", new NPCData());

        }

        [OnInspectorGUI]
        private void DrawInfoAboutSelectedItem()
        {
            NPCData selected = this.GetCurrentSelection().FirstOrDefault();

            if (selected != null)
            {
                GUILayout.Label("Name: " + selected.id);
                GUILayout.Label("Data: " + selected.modelId);
            }
        }
    }
    public partial class WorldEditor
    {
        static string dbName = "Test.db";
        string path = "URI=file:" + Application.streamingAssetsPath + "/" + dbName;
        SQLiteHelper db;

        TableProxy tableProxy;
        NpcProxy npcProxy;

        bool updated = false;


        [EditorMode] bool IsSettings = true;
        [EditorMode] bool IsNPC = false;
        [EditorMode] bool IsStatic = false;
        [EditorMode] bool IsQuest = false;

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


        void UpdateMode()
        {
            Type type = typeof(WorldEditor);
            FieldInfo[] Infos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            string Name = mode switch
            {
                EditorMode.Settings => GetVariableName(() => IsSettings),
                EditorMode.NPC => GetVariableName(() => IsNPC),
                EditorMode.Static => GetVariableName(() => IsStatic),
                EditorMode.Quest => GetVariableName(() => IsQuest),
                _ => IsSettings.ToString(),
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
           //data= GameObject.FindObjectOfType<WorldData>();
           // if (data == null)
           // {
           //     data = new WorldData();

           //     GameObject root = new GameObject();
           //     root.name = "WorldData";
           //     //root.AddComponent<WorldData>();

           // }
           // else
           // {
           //     npcs = data.npcs;
           //     statics= data.statics;
           //     inpendentQuests = data.inpendentQuests;
           // }



          
            mode = EditorMode.Settings;
            UpdateMode();

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