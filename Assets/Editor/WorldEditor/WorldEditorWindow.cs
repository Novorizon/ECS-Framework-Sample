using UnityEditor;
using Sirenix.OdinInspector.Editor;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor.SceneManagement;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Game;
using DataBase;
using Database;
using System.Linq;
using Sirenix.Utilities.Editor;

namespace WorldEditor
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class EditorModeAttribute : Attribute
    {
    }

    [Serializable]
    public partial class WorldEditor : OdinEditorWindow
    {
        public static WorldEditor Instance;


        [HideInInspector,]
        bool changed;

        WorldData data;


        EditorMode mode = EditorMode.Settings;


        [MenuItem("Tools/WorldEditor")]
        public static void ShowWindow()
        {
            GetWindow<WorldEditor>("World编辑器");
        }
        //protected override OdinMenuTree BuildMenuTree()
        //{
        //    OdinMenuTree tree = new OdinMenuTree();
        //    tree.Config.DrawSearchToolbar = true;
        //    //tree.Add("整包构建", new NPCEditor.NPCEditor());
        //    //tree.Add("热更构建", new PatchBuilderEditor());
        //    tree.AddAllAssetsAtPath("Characters", "Assets/Editor/WorldEditor/NPC", typeof(WorldNPC), true, true);
        //    //tree.AddAllAssetsAtPath("Characters1", "Assets/Plugins/Sirenix", typeof(NPCData), true, true);

        //    return tree;
        //}

        //protected override void OnBeginDrawEditors()
        //{
        //    var selected = this.MenuTree.Selection.FirstOrDefault();
        //    var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

        //    // Draws a toolbar with the name of the currently selected menu item.
        //    SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        //    {
        //        if (selected != null)
        //        {
        //            GUILayout.Label(selected.Name);
        //        }

        //        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
        //        {
        //            ScriptableObjectCreator.ShowDialog<WorldNPC>("Assets/Editor/WorldEditor/NPC", obj =>
        //            {
        //                obj.Name = obj.name;
        //                base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
        //            });
        //        }

        //        //if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Character")))
        //        //{
        //        //    ScriptableObjectCreator.ShowDialog<Character>("Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Character", obj =>
        //        //    {
        //        //        obj.Name = obj.name;
        //        //        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
        //        //    });
        //        //}
        //    }
        //    SirenixEditorGUI.EndHorizontalToolbar();
        //}

        protected override void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(30);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(EditorGUIUtility.IconContent("Terrain Icon", "Terrain."), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Settings;
                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("Prefab Icon"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                RefreshNPCPanel();
                mode = EditorMode.NPC;

                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("BuildSettings.Web.Small"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Static;

                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("SettingsIcon"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Quest;

                UpdateMode();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            base.OnGUI();

        }

        protected override void OnEnable()
        {


            if (Instance != null)
            {
                Instance.Close();
                Instance = null;
            }
            Instance = this;

            data = Transform.FindObjectOfType<WorldData>();
            if (data == null)
            {
                data = new WorldData();
                data.npcs = new List<WorldNPC>();
                data.statics = new List<WorldStatic>();
                data.inpendentQuests = new List<int>();
            }
            data.npcs = new List<WorldNPC>();
            data.statics = new List<WorldStatic>();
            data.inpendentQuests = new List<int>();
            //         npcs = data.npcs;
            //       statics = data.npcs;
            //inpendentQuests = new List<int>();

            hasUnsavedChanges = false;
            autoRepaintOnSceneChange = true;
            saveChangesMessage = "Click \"Cancel\" and export data to Json. \nOr click \"Save\" or \"Discard\" to close Level Editor.";

            db = new SQLiteHelper();
            Facade.RegisterProxy(new TableProxy());
            Facade.RegisterProxy(new NpcProxy());
            tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;

            tableProxy.RegisterTable<ModelTableAccess>();
            tableProxy.RegisterTable<NPCTableAccess>();


            db.Open(path);
            tableProxy.Load();
            db.Close();

            EditorApplication.update += Update;

        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;


            hasUnsavedChanges = false;
            autoRepaintOnSceneChange = false;
        }


        private void OnSceneGUI(SceneView sceneView)
        {
        }

        private void Update()
        {
            if (Instance == null)
                return;
        }

    }
}