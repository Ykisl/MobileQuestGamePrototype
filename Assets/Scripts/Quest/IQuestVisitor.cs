using Quest.ScriptableObjects.QuestNodes;
using System;

namespace Quest
{
    public interface IQuestVisitor
    {
        event Action<QuestNode> OnQuestNodeLoad;

        void Execute(QuestNode questNode);
        void Execute(TitleScreenQuestNode questNode);
        void Execute(SelectQuestNode questNode);
    }
}
