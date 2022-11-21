using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest.ScriptableObjects.QuestNodes
{
    [CreateAssetMenu(fileName = "QuestNode", menuName = "QuestNodes/BaseQuestNode", order = 1)]
    public class QuestNode : ScriptableObject
    {
        public string id;

        public virtual void Execute(IQuestVisitor questVisitor)
        {
            questVisitor.Execute(this);
        }
    }
}
