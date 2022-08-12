using DataBase;
using Game;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[Serializable]
public class WorldNPC
{
    public int id;
    public int npcId;
    public string Name;
    public string description;

    public GameObject model;
    public Vector3 position;
    public Vector3 rotation;
    public EntityLayerMask layerMask;
    public RelationshipLayer relationshipLayer;
    public InteractiveLayer interactiveLayer;

    public List<int> quests;

    public GameObject display;
}
