using DataBase;
using Mono.Data.Sqlite;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WorldEditor
{
    public partial class WorldEditor
    {
        WorldStatic worldStatic;

        [Title("Static 列表")]
        [HideLabel, ShowIf("IsStatic"), PropertySpace(SpaceBefore = 10), OnCollectionChanged("OnStaticChangedBefore", "OnStaticChangedAfter")]
        public List<WorldStatic> statics;


        public void EditStatic(WorldStatic npc)
        {
            worldStatic = npc;

            mode = EditorMode.NPC;
            UpdateMode();
        }

        public void OnStaticChangedBefore(CollectionChangeInfo info, object value)
        {
        }
        public void OnStaticChangedAfter(CollectionChangeInfo info, object value)
        {
        }
    }
}