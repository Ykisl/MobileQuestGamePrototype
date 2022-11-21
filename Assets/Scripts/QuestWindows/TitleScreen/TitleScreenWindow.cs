using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.WindowsManager;
using TMPro;
using Quest.ScriptableObjects.QuestNodes;
using System;
using UnityEngine.UI;
using Zenject;
using UI;

namespace QuestWindows.TitleScreen
{
    public class TitleScreenWindowInitParams
    {
        public TitleScreenQuestNode QuestNode;
        public Action OnMainButton;
    }
    public class TitleScreenWindow : Window<TitleScreenWindow>
    {
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] Button mainButton;
        [SerializeField] TextMeshProUGUI mainButtonText;

        private TitleScreenWindowInitParams _initParams;
        protected WindowManager _windowManager;

        [Inject]
        private void Construct(WindowManager windowManager)
        {
            _windowManager = windowManager;

            mainButton.onClick.AddListener(MainButtonClick);
        }

        protected override void WindowDestroy()
        {
            mainButton.onClick.RemoveListener(MainButtonClick);
        }

        public void Init(TitleScreenWindowInitParams initParams)
        {
            _initParams = initParams;

            if (_initParams == null)
            {
                return;
            }

            titleText.text = _initParams.QuestNode.Text;
            mainButtonText.text = _initParams.QuestNode.ButtonText;
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

        private void MainButtonClick()
        {
            if (_initParams == null)
            {
                return;
            }

            _initParams.OnMainButton?.Invoke();
        }
    }
}
