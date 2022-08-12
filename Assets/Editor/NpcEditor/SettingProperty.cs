using DataBase;
using Mono.Data.Sqlite;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCEditor
{
    [Serializable]
    public class NPCProperty
    {
        [ReadOnly]
        public int id;
        public string name;
        public string description;

        [HideInInspector]
        public NPCType type;
        [HideInInspector]
        public GameObject model;
        [HideLabel, PreviewField(50, ObjectFieldAlignment.Left)]
        public Sprite avatar;

        [Button("编辑")]
        public void Edit()
        {
            NPCEditor.Instance.Edit(this);
        }
        public NPCProperty()
        {
            name = "Default";
        }
    }

    public partial class NPCEditor
    {

        NPCProperty current;
        bool updated = false;

        [Title("属性")]
        [LabelText("NPC 列表"), ShowIf("IsSettings"), PropertySpace(SpaceBefore = 10), OnCollectionChanged("OnCollectionChangedBefore", "OnCollectionChangedAfter")]
        public List<NPCProperty> datas;


        [Button("Export"), ShowIf("IsSettings"), PropertySpace(SpaceBefore = 10)]
        public void Export()
        {
            for (int i = 0; i < datas.Count; i++)
            {
                NPCProperty property = datas[i];

                NPCData data = npcProxy.GetData(property.id);
                if (data != null)
                {
                    data.name = name;
                    data.description = description;
                    data.type = type;

                    string modelPath = AssetDatabase.GetAssetPath(property.model);
                    if (modelPath == null)
                    {
                        Debug.LogError("");
                        continue;
                    }

                    //data.modelId = model;

                }
            }
            current.id = id;
            current.name = name;
            current.description = description;
            current.model = model;// AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(model));
            current.type = type;
            current.avatar = avatar;
        }

        public void Edit(NPCProperty npc)
        {
            current = npc;

            id = npc.id;
            if (id == 0)
            {
            }

            name = npc.name;
            description = npc.description;
            type = npc.type;
            model = npc.model;
            avatar = npc.avatar;

            GameObject.DestroyImmediate(display);
            if (npc.model != null)
            {
                display = GameObject.Instantiate(npc.model);
                display.transform.position = Vector3.zero;
            }

            mode = EditorMode.Property;
            UpdateMode();
        }

        public void OnCollectionChangedBefore(CollectionChangeInfo info, List<NPCProperty> value)
        {
            if (info.ChangeType == CollectionChangeType.Add)
            {
                NPCProperty property = info.Value as NPCProperty;
                db.Open(path);
                SqliteDataReader reader = db.ExecuteQuery("select MAX(id) FROM NPC");
                if (reader.HasRows && reader.Read())
                {
                    property.id = reader.GetInt32(0) + 1;
                }
                db.Close();
                Debug.Log("增加：" + property.id);
            }
            else if (info.ChangeType == CollectionChangeType.RemoveIndex)
            {
                NPCProperty property = datas[info.Index];
                db.Open(path);
                db.ExecuteQuery("delete FROM NPC where id=" + property.id);
                db.Close();

                tableProxy.Load();
                Debug.Log("删除： " + property.id);
            }

        }
        public void OnCollectionChangedAfter(CollectionChangeInfo info, object value)
        {
            //Debug.Log("接收回调后变化的信息：\r\n " + info + "对应的集合实例： \r\n " + value);
        }
    }
}