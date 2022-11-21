using Quest.ScriptableObjects.QuestNodes;
using QuestWindows.SelectScreen;
using QuestWindows.TitleScreen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quest
{
    public class QuestWindowVisitor : MonoBehaviour, IQuestVisitor
    {
        public event Action<QuestNode> OnQuestNodeLoad;

        private TitleScreenWindow _titleScreenWindow;
        private SelectScreenWindow _selectScreenWindow;

        [Inject]
        private void Construct(
            TitleScreenWindow titleScreenWindow,
            SelectScreenWindow selectScreenWindow
            )
        {
            _titleScreenWindow = titleScreenWindow;
            _selectScreenWindow = selectScreenWindow;
        }

        public void Execute(QuestNode questNode)
        {
            LoadNode(null);
        }

        public void Execute(TitleScreenQuestNode questNode)
        {
            var titleScreenWindowInitParams = new TitleScreenWindowInitParams()
            {
                QuestNode = questNode,
                OnMainButton = () => 
                {
                    _titleScreenWindow.Close();
                    LoadNode(questNode.NextNode);
                }
            };

            _titleScreenWindow.Init(titleScreenWindowInitParams);
            _titleScreenWindow.Show();
        }

        public void Execute(SelectQuestNode questNode)
        {
            var selectScreenWindowInitParams = new SelectScreenWindowInitParams()
            {
                QuestNode = questNode,
                OnButtonSelected = (QuestButtonSelect buttonSelect) =>
                {
                    _selectScreenWindow.Close();
                    LoadNode(buttonSelect.NextNode);
                }
            };

            _selectScreenWindow.Init(selectScreenWindowInitParams);
            _selectScreenWindow.Show();
        }

        private void LoadNode(QuestNode questNode)
        {
            OnQuestNodeLoad?.Invoke(questNode);
        }
    }
}
