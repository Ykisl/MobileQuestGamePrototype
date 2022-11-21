using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest.ScriptableObjects.QuestNodes
{
    [CreateAssetMenu(fileName = "TitleScreenQuestNode", menuName = "QuestNodes/TitleScreenQuestNode", order = 1)]
    public class TitleScreenQuestNode : QuestNode
    {
        public string Text;
        public string ButtonText;
        [Space]
        public QuestNode NextNode;

        public override void Execute(IQuestVisitor questVisitor)
        {
            questVisitor.Execute(this);
        }
    }
}
