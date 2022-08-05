using DataBase;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [Serializable]
    public class NPCProperty
    {
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
        [LabelText("NPC 列表"), ShowIf("IsSettings"), PropertySpace(SpaceBefore = 10)]
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
               
                    data.modelId = model;

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
            GameObject.DestroyImmediate(display);
            display = GameObject.Instantiate(npc.model);
            display.transform.position = Vector3.zero;

            id = npc.id;
            name = npc.name;
            description = npc.description;
            type = npc.type;
            model = npc.model;
            avatar = npc.avatar;

            mode = EditorMode.Property;
            UpdateMode();
        }
    }
}