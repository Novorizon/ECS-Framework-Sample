using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldData : ScriptableObject
{
    public int id;
    public string name;
    public List<WorldNPC> npcs;
    public List<WorldStatic> statics;
    public List<int> inpendentQuests;
}
