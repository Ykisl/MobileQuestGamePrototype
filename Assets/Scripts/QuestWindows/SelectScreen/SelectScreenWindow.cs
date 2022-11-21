using Plugins.WindowsManager;
using Quest.ScriptableObjects.QuestNodes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace QuestWindows.SelectScreen
{
    public class SelectScreenWindowInitParams
    {
        public SelectQuestNode QuestNode;
        public Action<QuestButtonSelect> OnButtonSelected;
    }
    public class SelectScreenWindow : Window<SelectScreenWindow>
    {
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] Button firstButton;
        [SerializeField] TextMeshProUGUI firstButtonText;
        [SerializeField] Button secondButton;
        [SerializeField] TextMeshProUGUI secondButtonText;

        private SelectScreenWindowInitParams _initParams;
        protected WindowManager _windowManager;

        [Inject]
        private void Construct(WindowManager windowManager)
        {
            _windowManager = windowManager;

            firstButton.onClick.AddListener(FirstButtonClick);
            secondButton.onClick.AddListener(SecondButtonClick);
        }

        protected override void WindowDestroy()
        {
            firstButton.onClick.RemoveListener(FirstButtonClick);
            secondButton.onClick.RemoveListener(SecondButtonClick);
        }

        public void Init(SelectScreenWindowInitParams initParams)
        {
            _initParams = initParams;

            if (_initParams == null)
            {
                return;
            }

            titleText.text = _initParams.QuestNode.Text;

            InitSelectButton(firstButtonText, _initParams.QuestNode.FirstSelect);
            InitSelectButton(secondButtonText, _initParams.QuestNode.SecondSelect);
        }

        public void Show()
        {
            if (_initParams == null)
            {
                return;
            }

            if (_windowManager.GetWindow(WindowId) == null)
            {
                _windowManager.ShowCreatedWindow(WindowId);
            }
        }

        public override void Activate(bool immediately = false)
        {
            ActivatableState = ActivatableState.Active;
            gameObject.SetActive(true);
        }

        public override void Deactivate(bool immediately = false)
        {
            ActivatableState = ActivatableState.Inactive;
            gameObject.SetActive(false);
        }

        private void InitSelectButton(TextMeshProUGUI buttonText, QuestButtonSelect questButton)
        {
            buttonText.text = questButton.Text;
        }

        private void FirstButtonClick()
        {
            if (_initParams == null)
            {
                return;
            }

            var buttonSelect = _initParams.QuestNode.FirstSelect;
            _initParams.OnButtonSelected?.Invoke(buttonSelect);
        }

        private void SecondButtonClick()
        {
            if (_initParams == null)
            {
                return;
            }

            var buttonSelect = _initParams.QuestNode.SecondSelect;
            _initParams.OnButtonSelected?.Invoke(buttonSelect);
        }
    }
}
