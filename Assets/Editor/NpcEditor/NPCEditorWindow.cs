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

namespace NPCEditor
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class EditorModeAttribute : Attribute
    {
    }

    [Serializable]
    public partial class NPCEditor : OdinEditorWindow
    {
        public static NPCEditor Instance;
        [HideInInspector,]
        bool changed;

        List<NPCData> npcs;

        public static string ScenePath = "Assets/Plugins/HexMap/Editor/Scenes/";
        public static string MapPath = "Assets/Main/Maps/";
        public static string MapsPath = "/Main/Maps/";

        string MapNameLast;
        string MapName { get { return id + "_" + name; } }
        string MapHexTerrainName { get { return MapName + ".asset"; } }



        [MenuItem("Tools/NPCEditor")]
        public static void ShowWindow()
        {
            GetWindow<NPCEditor>("NPCEditor");
        }


        protected override void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(30);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(EditorGUIUtility.IconContent("Terrain Icon", "Terrain."), GUILayout.Width(50), GUILayout.Height(30)))
            {
                InitLIst();
                mode = EditorMode.Settings;
                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("Prefab Icon"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Property;

                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("SettingsIcon"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Model;

                UpdateMode();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            base.OnGUI();

            if (changed)
            {
                //EditorApplication.delayCall += () => { ReCreate(); };
                //ReCreate();
                changed = false;
            }

        }

        protected override void OnEnable()
        {
            if (Instance != null)
            {
                Instance.Close();
                Instance = null;
            }
            Instance = this;

            db = new SQLiteHelper();


            Facade.RegisterProxy(new TableProxy());
            Facade.RegisterProxy(new NpcProxy());
            tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;

            tableProxy.RegisterTable<ModelTableAccess>();
            tableProxy.RegisterTable<NPCTableAccess>();
            access = tableProxy.GetAccess<ModelTableAccess>();


            db.Open(path);
            tableProxy.Load();
            db.Close();

            hasUnsavedChanges = false;
            autoRepaintOnSceneChange = true;
            saveChangesMessage = "Click \"Cancel\" and export data to Json. \nOr click \"Save\" or \"Discard\" to close Level Editor.";


            InitLIst();

            EditorApplication.update += Update;
            SceneView.duringSceneGui += OnSceneGUI;
        }


        private void OnDisable()
        {
            EditorApplication.update -= Update;
            //HexMapMgr.Instance.UnloadMap();

            id = 0;
            name = "";
            GameObject.DestroyImmediate(display);
            display=null;

            hasUnsavedChanges = false;
            autoRepaintOnSceneChange = false;
        }


        private void OnSceneGUI(SceneView sceneView)
        {
            //HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));//为scene响应添加默认事件,用来屏蔽以前的点击选中物体
            Event currentEvent = Event.current;

            switch (mode)
            {
                case EditorMode.Property:
                    break;

                case EditorMode.Model:
                    break;

            }

        }

        private void Update()
        {
            if (Instance == null)
                return;
        }
    }
}