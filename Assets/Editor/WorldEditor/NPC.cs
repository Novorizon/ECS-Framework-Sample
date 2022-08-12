using DataBase;
using Game;
using Mono.Data.Sqlite;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace WorldEditor
{
    [Serializable]
    public class WorldNPCAsset : ScriptableObject
    {
        public int id;
    }



    public partial class WorldEditor
    {
        Dictionary<int, NPCData> npcDic;
        WorldNPC worldNpc;

        [ReadOnly, HideLabel, PreviewField(50), HorizontalGroup("pre", 50, LabelWidth = 50), VerticalGroup("pre/avatar")]
        public Sprite avatar;

        [HideLabel, VerticalGroup("pre/id")]
        public int id;

        [HideLabel, VerticalGroup("pre/id")]
        public new string name;

        [ReadOnly] public int npcId;
        [ReadOnly] public string npcName;
        [ReadOnly] public string description;
        [ReadOnly] public int modelId;
        [ReadOnly] public int level;
        [ReadOnly] public NPCType type;
        [ReadOnly] public GameObject model;
        [ShowIf("IsNPC"), PropertySpace(SpaceBefore = 10), OnValueChanged("OnNPCChanged")]
        public NPCAsset npcAsset;

        public Vector3 position;
        public Vector3 rotation;
        public EntityLayerMask layerMask;
        public RelationshipLayer relationshipLayer;
        public InteractiveLayer interactiveLayer;
        public List<int> quests;

        //[LabelText("NPC 列表"), AssetSelector, ShowIf("IsNPC"), PropertySpace(SpaceBefore = 10), OnCollectionChanged("OnNPCChangedBefore", "OnNPCChangedAfter")]
        //[HorizontalGroup(400)]
        //public List<WorldNPC> npcs;

        [ShowIf("IsNPC"), PropertySpace(SpaceBefore = 10)]
        [HorizontalGroup("NPCTree", 400)]
        public OdinMenuTree tree;


        [Button("Add"), ShowIf("IsNPC"),]
        public void AddNPC()
        {
            id = 0;
            name = "Default";
            description = "";
            level = 0;
            this.type = 0;
            avatar = null;
            model = null;

            WorldNPCAsset asset = ScriptableObject.CreateInstance<WorldNPCAsset>();
            asset.id = 0;
            AssetDatabase.CreateAsset(asset, "Assets/Editor/WorldEditor/WorldNPC/" + name + ".asset");

            worldNpc = new WorldNPC();
            worldNpc.id = id;
            worldNpc.npcId = id;
            worldNpc.Name = "";
            worldNpc.description = "";
            worldNpc.model = null;
            worldNpc.display = null;

            tree.Add(name, asset);
            //tree.GetMenuItem(name);
            tree.Selection.Add(tree.GetMenuItem(name));
        }


        [Button("Delete"), ShowIf("IsNPC"),]
        public void DeleteNPC()
        {
            WorldNPCAsset asset = (WorldNPCAsset)tree.Selection.SelectedValue;
            if (asset == null)
                return;

            if (asset.id != worldNpc.id)
                Debug.LogError(asset.id != worldNpc.id);

            WorldNPC world = data.npcs.Find(a => a.id == asset.id);
            if (world == null)
                return;
            world.model = null;
            GameObject.DestroyImmediate(world.display);
            data.npcs.Remove(world);

            avatar = null;
            id = 0;
            name = "Default";
            description = "";
            level = 0;
            this.type = 0;
            model = null;

            worldNpc = new WorldNPC();

            //ScriptableObject.DestroyImmediate(asset, true);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset));
            AssetDatabase.Refresh();
            //tree.AddAllAssetsAtPath("NPC", "Assets/Editor/WorldEditor/WorldNPC/", typeof(WorldNPCAsset), true, true);
            //tree.MarkDirty();
            //tree.UpdateMenuTree();
            RefreshNPCPanel();
        }


        [Button("Apply"), ShowIf("IsNPC"),]
        public void ApplyNPC()
        {
            WorldNPCAsset asset = (WorldNPCAsset)tree.Selection.SelectedValue;
            if (asset == null)
                return;

            if (asset.id != worldNpc.id)
                Debug.LogError(asset.id != worldNpc.id);

            WorldNPC world = data.npcs.Find(a => a.id == asset.id);
            if (world == null)
            {
                world = new WorldNPC();
                world.npcId = worldNpc.npcId;
                world.Name = worldNpc.Name;
                world.description = worldNpc.description;
                world.model = worldNpc.model;
            }

            data.npcs.Add(world);
        }

        [Button("刷新NPC Panel"), ShowIf("IsNPC"),]
        public void RefreshNPCPanel()
        {
            InitNPC();
            InitWorldNPC();

            tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;
            //tree.AddAllAssetsAtPath("NPC", "Assets/Editor/WorldEditor/NPC/", typeof(WorldNPCAsset), true, true);
            tree.AddAllAssetsAtPath("NPC", "Assets/Editor/WorldEditor/WorldNPC/", typeof(WorldNPCAsset), true, true);

            var selected = tree.Selection.FirstOrDefault();
            var toolbarHeight = tree.Config.SearchToolbarHeight;

            tree.Selection.SelectionChanged += OnSelectionChanged;
            tree.DrawMenuTree();
            tree.HandleKeyboardMenuNavigation();
        }

        private void OnSelectionChanged(SelectionChangedType type)
        {
            if (type == SelectionChangedType.ItemAdded)
            {
                WorldNPCAsset asset = (WorldNPCAsset)tree.Selection.SelectedValue;
                if (asset == null)
                    return;

                WorldNPC world = data.npcs.Find(a => a.id == asset.id);
                if (world == null)
                    return;

                worldNpc = world;
                NPCData npc = tableProxy.GetData<NPCData>(worldNpc.npcId);

                id = npc.id;
                name = npc.name;
                description = npc.description;
                level = npc.level;
                this.type = npc.type;

                avatar = AssetDatabase.LoadAssetAtPath<Sprite>(npc.avatar);
                worldNpc.model = AssetDatabase.LoadAssetAtPath<GameObject>(npc.model);
                if (!worldNpc.display)
                {
                    GameObject.DestroyImmediate(worldNpc.display);
                    worldNpc.display = GameObject.Instantiate(worldNpc.model);
                }
                model = worldNpc.display;
            }

        }

        private void OnNPCChanged()
        {
            NPCData npc = tableProxy.GetData<NPCData>(npcAsset.id);
            if (npc == null)
                return;
            id = npc.id;
            name = npc.name;
            description = npc.description;
            level = npc.level;
            this.type = npc.type;

            worldNpc.npcId = npc.id;
            avatar = AssetDatabase.LoadAssetAtPath<Sprite>(npc.avatar);
            worldNpc.model = AssetDatabase.LoadAssetAtPath<GameObject>(npc.model);
            if (!worldNpc.display)
            {
                GameObject.DestroyImmediate(worldNpc.display);
                worldNpc.display = GameObject.Instantiate(worldNpc.model);
            }
            model = worldNpc.display;

        }

        private void InitNPC()
        {
            if (!updated)
            {
                NPCTableAccess access = tableProxy.GetAccess<NPCTableAccess>();
                npcDic = access.GetDatas();

                foreach (var item in npcDic)
                {
                    NPCAsset npc = ScriptableObject.CreateInstance<NPCAsset>();
                    npc.id = item.Value.id;
                    npc.name = item.Value.name;
                    npc.description = item.Value.description;
                    npc.avatar = AssetDatabase.LoadAssetAtPath<Sprite>(item.Value.avatar);
                    AssetDatabase.CreateAsset(npc, "Assets/Editor/WorldEditor/NPC/" + npc.name + ".asset");
                }
                updated = true;
            }
        }

        private void InitWorldNPC()
        {
            for (int i = 0; i < data.npcs.Count; i++)
            {
                WorldNPC npc = data.npcs[i];
                WorldNPCAsset asset = ScriptableObject.CreateInstance<WorldNPCAsset>();
                asset.id = npc.id;


                AssetDatabase.CreateAsset(asset, "Assets/Editor/WorldEditor/WorldNPC/" + npc.Name + ".asset");
            }
        }


        public void OnNPCChangedBefore(CollectionChangeInfo info, List<WorldNPCAsset> value)
        {
            //if (info.ChangeType == CollectionChangeType.Add)
            //{
            //    WorldEditorNPC npc = info.Value as WorldEditorNPC;
            //    db.Open(path);
            //    SqliteDataReader reader = db.ExecuteQuery("select MAX(id) FROM NPC");
            //    if (reader.HasRows && reader.Read())
            //    {
            //        property.id = reader.GetInt32(0) + 1;
            //    }
            //    db.Close();
            //    Debug.Log("增加：" + property.id);
            //}
            //else if (info.ChangeType == CollectionChangeType.RemoveIndex)
            //{
            //    NPCProperty property = datas[info.Index];
            //    db.Open(path);
            //    db.ExecuteQuery("delete FROM NPC where id=" + property.id);
            //    db.Close();

            //    tableProxy.Load();
            //    Debug.Log("删除： " + property.id);
            //}

        }
        public void OnNPCChangedAfter(CollectionChangeInfo info, object value)
        {
        }

        public void RefreshNPCTree()
        {
            tree.AddAllAssetsAtPath("NPC", "Assets/Editor/WorldEditor/WorldNPC/", typeof(WorldNPCAsset), true, true);

            //var selected = tree.Selection.FirstOrDefault();
            //var toolbarHeight = tree.Config.SearchToolbarHeight;

            //tree.Selection.SelectionChanged += OnSelectionChanged;
            //tree.DrawMenuTree();
            //tree.HandleKeyboardMenuNavigation();
        }
    }
}