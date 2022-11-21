using Quest.ScriptableObjects.QuestNodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 1)]
    public class Quest : ScriptableObject
    {
        public string Id;
        public string Name;
        [Space]
        public QuestNode StartQuestNode;
    }
}
