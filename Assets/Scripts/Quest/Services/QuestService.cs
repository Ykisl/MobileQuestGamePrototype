using Quest.ScriptableObjects.QuestNodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using QuestObject = Quest.ScriptableObjects.Quest;

namespace Quest.Service
{
    public class QuestService : MonoBehaviour
    {
        public event Action<QuestObject> OnQuestStarted;
        public event Action<QuestObject> OnQuestFinished;
        public event Action<QuestNode> OnQuestNodeChanged;

        private QuestWindowVisitor _questWindowVisitor;

        private QuestObject _currentQuest;
        private QuestNode _currentQuestNode;

        [Inject]
        private void Construct(QuestWindowVisitor questWindowVisitor)
        {
            _questWindowVisitor = questWindowVisitor;

            _questWindowVisitor.OnQuestNodeLoad += QuestNodeLoad;
        }

        private void OnDestroy()
        {
            _questWindowVisitor.OnQuestNodeLoad -= QuestNodeLoad;
        }

        public void Init(QuestObject quest)
        {
            StartQuest(quest);
        }

        private void StartQuest(QuestObject quest)
        {
            _currentQuest = quest;
            OnQuestStarted?.Invoke(_currentQuest);

            ExecuteQuestNode(quest.StartQuestNode);
        }

        private void FinishQuest()
        {
            OnQuestFinished?.Invoke(_currentQuest);
        }

        private void ExecuteQuestNode(QuestNode questNode)
        {
            _currentQuestNode = questNode;

            if(_currentQuestNode == null)
            {
                FinishQuest();
                return;
            }

            questNode.Execute(_questWindowVisitor);
        }

        private void QuestNodeLoad(QuestNode questNode)
        {
            ExecuteQuestNode(questNode);
        }
    }
}
