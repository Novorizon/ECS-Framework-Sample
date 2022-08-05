//using UnityEditor;
//using Sirenix.OdinInspector.Editor;
//using UnityEngine.SceneManagement;
//using UnityEngine;
//using System;
//using UnityEditor.SceneManagement;
//using System.Text.RegularExpressions;
//using System.Collections.Generic;

//namespace WorldMapEditor
//{
//    [Serializable]
//    public partial class WorldEditor : OdinEditorWindow
//    {
//        public static WorldEditor Instance;


//        [HideInInspector,]
//        bool changed;

//        //List<HexCell> cells;

//        public static string ScenePath = "Assets/Plugins/HexMap/Editor/Scenes/";
//        public static string MapPath = "Assets/Main/Maps/";
//        public static string MapsPath = "/Main/Maps/";

//        string MapNameLast;
//        string MapName { get { return id + "_" + name; } }
//        string MapHexTerrainName { get { return MapName + ".asset"; } }


//        string MapHexTerrainPath { get { return MapPath + MapName + "/" + MapHexTerrainName; } }
//        string MapJsonName { get { return MapName + ".json"; } }
//        string MapJsonPath { get { return MapPath + MapName + "/" + MapJsonName; } }

//        string MapScenePath { get { return MapPath + MapName + "/" + MapName + ".unity"; } }
//        string MaterialPath { get { return MapPath + MapName + "/Material/"; } }
//        string MaterialApplicationPath { get { return MapsPath + MapName + "/Material/"; } }


//        public static readonly GUIContent newContent = new GUIContent(_new_ico, "new.");

//        //[MenuItem("Tools/HexMapEditor")]
//        public static void ShowWindow()
//        {
//            GetWindow<WorldEditor>("World编辑器");
//        }


//        protected override void OnGUI()
//        {
//            GUILayout.BeginVertical();
//            GUILayout.Space(30);
//            GUILayout.EndVertical();

//            GUILayout.BeginHorizontal();
//            GUILayout.FlexibleSpace();

//            if (GUILayout.Button(EditorGUIUtility.IconContent("Terrain Icon", "Terrain."), GUILayout.Width(50), GUILayout.Height(30)))
//            {
//                mode = EditorMode.Brush;//update
//                UpdateMode();
//            }
//            if (GUILayout.Button(EditorGUIUtility.IconContent("Prefab Icon"), GUILayout.Width(50), GUILayout.Height(30)))
//            {
//                mode = EditorMode.Feature;

//                UpdateMode();
//            }
//            if (GUILayout.Button(EditorGUIUtility.IconContent("BuildSettings.Web.Small"), GUILayout.Width(50), GUILayout.Height(30)))
//            {
//                mode = EditorMode.Pathfinding;

//                UpdateMode();
//            }
//            //if (GUILayout.Button(EditorGUIUtility.IconContent("ViewToolOrbit"), GUILayout.Width(50), GUILayout.Height(30)))
//            //{
//            //    mode = EditorMode.FogOfWar;

//            //    UpdateMode();
//            //}
//            if (GUILayout.Button(EditorGUIUtility.IconContent("SettingsIcon"), GUILayout.Width(50), GUILayout.Height(30)))
//            {
//                mode = EditorMode.Settings;

//                UpdateMode();
//            }
//            GUILayout.FlexibleSpace();
//            GUILayout.EndHorizontal();
//            base.OnGUI();

//            if (changed)
//            {
//                EditorApplication.delayCall += () => { ReCreate(); };
//                //ReCreate();
//                changed = false;
//            }

//        }

//        private new void OnEnable()
//        {
//            if (Instance != null)
//            {
//                Instance.Close();
//                Instance = null;
//            }
//            Instance = this;

//            //Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

//            hasUnsavedChanges = false;
//            autoRepaintOnSceneChange = true;
//            saveChangesMessage = "Click \"Cancel\" and export data to Json. \nOr click \"Save\" or \"Discard\" to close Level Editor.";

//            //_new_ico = (Texture2D)AssetDatabase.LoadAssetAtPath("Packages/com.guanjinbiao.hexmap/Assets/Texture/Icons/Advanced.png", typeof(Texture2D));
//            //SetTextureProperties("Packages/com.guanjinbiao.hexmap/Assets/Texture/Noise.png", true);
//            //Noise = (Texture2D)AssetDatabase.LoadAssetAtPath("Packages/com.guanjinbiao.hexmap/Assets/Texture/Noise.png", typeof(Texture2D));
//            //Noise = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/hexmap/Assets/Texture/Noise.png", typeof(Texture2D));

//            HexMapMgr.Instance.Data = new HexMapData();

//            EditorApplication.update += Update;
//            SceneView.duringSceneGui += OnSceneGUI;
//            EditorSceneManager.sceneClosing += (scene, removeing) =>
//            {
//                OnSceneClosing();
//            };
//            //Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Packages/com.unity.images-library/Example/Images/image.png", typeof(Texture2D));
//            //_new_ico = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/compass.png");
//            //new_ico = new GUIContent(_new_ico, "地形");
//        }


//        private void OnDisable()
//        {
//            EditorApplication.update -= Update;
//            //HexMapMgr.Instance.UnloadMap();

//            id = 0;
//            name = "";

//            hasUnsavedChanges = false;
//            autoRepaintOnSceneChange = false;
//        }

//        EditorMode mode = EditorMode.Brush;

//        void UpdateMode()
//        {
//            Type type = typeof(HexMapEditor);
//            FieldInfo[] Infos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

//            string Name = mode switch
//            {
//                EditorMode.Brush => GetVariableName(() => IsBrush),
//                EditorMode.Feature => GetVariableName(() => IsFeature),
//                EditorMode.Pathfinding => GetVariableName(() => IsPathfinding),
//                EditorMode.FogOfWar => GetVariableName(() => IsFogOfWar),
//                EditorMode.Settings => GetVariableName(() => IsSettings),
//                _ => IsBrush.ToString(),
//            };

//            for (int i = 0; i < Infos.Length; i++)
//            {
//                EditorModeAttribute a = Attribute.GetCustomAttribute(Infos[i], typeof(EditorModeAttribute)) as EditorModeAttribute;
//                if (a != null)
//                {
//                    Infos[i].SetValue(this, false);
//                    if (Name == Infos[i].Name)
//                    {
//                        Infos[i].SetValue(this, true);
//                    }
//                }
//            }

//        }

//        private void OnSceneClosing()
//        {
//            if (!EditorApplication.isPlaying)
//            {
//                if (state != State.None)
//                {
//                    if (ValidateName(MapNameLast))
//                    {
//                        if (ValidateMapChanged())
//                        {
//                            int i = id;
//                            string n = name;
//                            id = SplitName(ref MapNameLast);
//                            name = MapNameLast;
//                            //ExportTexture(false);
//                            SetTextureProperties(HexMapMgr.Instance.TerrainTypeTexture, false);
//                            SetTextureProperties(HexMapMgr.Instance.TerrainOpacityTexture, false);
//                            SetTextureProperties(HexMapMgr.Instance.RoadTexture, false);
//                            id = i;
//                            name = n;
//                        }
//                        else
//                        {
//                            isEditor = false;
//                        }
//                    }
//                }
//                MapNameLast = MapName;
//            }
//            state = State.Open;
//        }

//        private void OnSceneChanged(Scene a, Scene b)
//        {
//            Debug.LogError(a.name);
//            Debug.LogError(b.name);
//            if (!EditorApplication.isPlaying)
//            {
//                if (state != State.None)
//                {
//                    if (ValidateMapChanged())
//                    {
//                        int i = id;
//                        string n = name;
//                        id = SplitName(ref MapNameLast);
//                        name = MapNameLast;
//                        //ExportTexture(false);
//                        id = i;
//                        name = n;
//                    }
//                }
//                MapNameLast = MapName;
//            }
//            state = State.Open;
//        }

//        private void OnSceneGUI(SceneView sceneView)
//        {
//            if (!isEditor)
//            {
//                //Debug.LogError("请使用HexMapEditor创建或加载地图");
//                return;
//            }
//            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));//为scene响应添加默认事件,用来屏蔽以前的点击选中物体
//            Event currentEvent = Event.current;

//            switch (mode)
//            {
//                case EditorMode.Brush:
//                    UpdateBrush();
//                    if (lastMode != mode && HexMapMgr.Instance.TerrainMaterial != null)
//                        HexMapMgr.Instance.TerrainMaterial?.DisableKeyword("GRID_ON");
//                    break;

//                case EditorMode.Feature:
//                    UpdateBrush();
//                    if (lastMode != mode && HexMapMgr.Instance.TerrainMaterial != null)
//                        HexMapMgr.Instance.TerrainMaterial?.DisableKeyword("GRID_ON");
//                    break;

//                case EditorMode.Pathfinding:
//                    UpdatePathfinding();
//                    if (lastMode != mode && HexMapMgr.Instance.TerrainMaterial != null)
//                        HexMapMgr.Instance.TerrainMaterial?.EnableKeyword("GRID_ON");
//                    break;

//                case EditorMode.FogOfWar:
//                    UpdateFogOfWar();
//                    if (lastMode != mode && HexMapMgr.Instance.TerrainMaterial != null)
//                        HexMapMgr.Instance.TerrainMaterial?.EnableKeyword("GRID_ON");
//                    break;
//            }
//            if (lastMode == mode)
//                return;
//            lastMode = mode;
//            UpdateLabel();

//            if (HexMapMgr.Instance.Root)
//                EditorUtility.SetDirty(HexMapMgr.Instance.Root);
//        }

//        private void Update()
//        {
//            if (Instance == null)
//                return;
//        }

//        private bool ValidateName(string name)
//        {
//            if (name == null)
//                return false;
//            string[] s = name.Split('_');
//            return (s.Length == 2 && IsNumber(s[0]));
//        }
//        private int SplitName(ref string name)
//        {
//            string[] s = name.Split('_');
//            if (s.Length == 2 && IsNumber(s[0]))
//            {
//                int Id = int.Parse(s[0]);
//                name = s[1];
//                return Id;
//            }
//            return -1;
//        }

//        private bool ValidateMapChanged()
//        {

//            string sceneName = SceneManager.GetActiveScene().name;
//            return MapName != MapNameLast || MapName != sceneName;
//        }


//        static bool IsNumber(string input)
//        {
//            Regex reg = new Regex("^[1-9]\\d*$");
//            return reg.IsMatch(input);
//        }
//    }
//}