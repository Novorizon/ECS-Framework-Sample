using DataBase;
using Sirenix.OdinInspector;
using UnityEngine;
public class NPCAsset : ScriptableObject
{
    [HorizontalGroup("pre", 50, LabelWidth = 50)]
    [VerticalGroup("pre/avatar")]
    [ReadOnly, HideLabel, PreviewField(50)]
    public Sprite avatar;

    [ReadOnly, HideLabel, VerticalGroup("pre/id")]
    public int id;

    [ReadOnly, HideLabel, VerticalGroup("pre/id")]
    public string name;

    [FoldoutGroup("Data")][ReadOnly] public string description;
    [FoldoutGroup("Data")][ReadOnly] public string model;
    [FoldoutGroup("Data")][ReadOnly] public int level;
    [FoldoutGroup("Data")][ReadOnly] public NPCType type;

}