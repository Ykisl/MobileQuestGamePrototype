using Core.ContextLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using QuestObject = Quest.ScriptableObjects.Quest;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DebugScene
{
    public class DebugQuestView : MonoBehaviour
    {
        [SerializeField] private QuestObject quest;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button startButton;

        private ContextLoaderService _contextLoaderService;

        [Inject]
        private void Construct(ContextLoaderService contextLoaderService)
        {
            _contextLoaderService = contextLoaderService;

            startButton.onClick.AddListener(StartButtonClicked);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(StartButtonClicked);
        }

        private void OnValidate()
        {
            UpdateNameText();
        }

        private void UpdateNameText()
        {
            if(nameText == null)
            {
                return;
            }

            var text = quest == null ? "EmptyQuest" : quest.Name;
            nameText.text = text;
        }

        private void StartButtonClicked()
        {
            if(quest == null)
            {
                return;
            }

            _contextLoaderService.LoadScene("Scenes/QuestScene", LoadSceneMode.Additive, quest);
        }
    }
}
