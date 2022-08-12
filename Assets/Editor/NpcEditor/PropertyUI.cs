using DataBase;
using Mono.Data.Sqlite;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace NPCEditor
{
    public partial class NPCEditor
    {
        GameObject display;

        [Title("属性")]
        [ReadOnly]
        [LabelText("id"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public int id = 0;

        [LabelText("名称"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        new public string name = "Default";

        [LabelText("描述"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public string description;

        [LabelText("类型"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public NPCType type;

        [LabelText("模型"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10), OnValueChanged("ChangeModel")]
        [HideLabel, PreviewField(150, ObjectFieldAlignment.Left)]
        public GameObject model;

        [HideLabel, PreviewField(100, ObjectFieldAlignment.Left)]
        [LabelText("头像"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public Sprite avatar;

        [Button("Apply"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public void Apply()
        {
            current.id = id;
            current.name = name;
            current.description = description;
            current.model = model;// AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(model));
            current.type = type;
            current.avatar = avatar;


            db.Open(path);
            NPCData data = tableProxy.GetData<NPCData>(current.id);
            if (data == null)
            {
                data = new NPCData();
                data.id = id;
                db.ExecuteQuery("insert into NPC (id) values (" + data.id.ToString() + ")");
            }

            data.name = name;
            data.description = description;
            data.type = type;
            data.model = AssetDatabase.GetAssetPath(model);
            data.avatar = AssetDatabase.GetAssetPath(avatar);
            if (data.avatar == null)
                data.avatar = "";

            db.UpdateInto("npc", new string[] { "name", "description", "model", "avatar", "type", "level" }, new string[] { "'" + name + "'", "'"+ description + "'", "'" +
                data.model+ "'", "'" + data.avatar + "'", ((int)type).ToString(), "1" }, "id", data.id.ToString());

            db.Close();
            tableProxy.Load();
        }

        public void ChangeModel()
        {
            GameObject.DestroyImmediate(display);
            display = GameObject.Instantiate(model);
            display.transform.position = Vector3.zero;
            current.id = id;
            current.name = name;
            current.description = description;
            current.model = model;
            current.type = type;
            current.avatar = avatar;
        }
        public void ChangeName()
        {

        }
    }
}