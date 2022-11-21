using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest.ScriptableObjects.QuestNodes
{
    [Serializable]
    public class QuestButtonSelect
    {
        public string Text;
        public QuestNode NextNode;
    }

    [CreateAssetMenu(fileName = "SelectScreenQuestNode", menuName = "QuestNodes/SelectScreenQuestNode", order = 1)]
    public class SelectQuestNode : QuestNode
    {
        public string Text;
        [Space]
        public QuestButtonSelect FirstSelect;
        public QuestButtonSelect SecondSelect;

        public override void Execute(IQuestVisitor questVisitor)
        {
            questVisitor.Execute(this);
        }
    }
}
