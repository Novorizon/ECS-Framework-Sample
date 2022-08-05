using DataBase;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public partial class NPCEditor
    {
        GameObject display;

        [Title("属性")]
        [LabelText("id"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public int id = 0;

        [LabelText("名称"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        new public string name = "Default";

        [LabelText("描述"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public string description;

        [LabelText("类型"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10)]
        public NPCType type;

        [LabelText("模型"), ShowIf("IsProperty"), PropertySpace(SpaceBefore = 10),OnValueChanged("ChangeModel")]
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
    }
}