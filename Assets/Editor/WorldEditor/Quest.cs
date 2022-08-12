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
        int inpendentQuest;

        [Title("Quest 列表")]
        [HideLabel, ShowIf("IsQuest"), PropertySpace(SpaceBefore = 10), OnCollectionChanged("OnQuestChangedBefore", "OnQuestChangedAfter")]
        public List<int> inpendentQuests;


        public void EditStatic(int npc)
        {
            inpendentQuest = npc;


            mode = EditorMode.NPC;
            UpdateMode();
        }

        public void OnQuestChangedBefore(CollectionChangeInfo info, object value)
        {
        }

        public void OnQuestChangedAfter(CollectionChangeInfo info, object value)
        {
        }
    }
}