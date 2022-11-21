using Core.ContextLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest.ScriptableObjects;
using QuestObject = Quest.ScriptableObjects.Quest;
using Quest.Service;
using Zenject;
using UnityEngine.SceneManagement;

namespace Quest.Context
{
    public class QuestContextLoader : ContextLoader
    {
        private QuestService _questService;

        [Inject]
        private void Construct(QuestService questService)
        {
            _questService = questService;

            _questService.OnQuestFinished += SceneQuestFinished;
        }

        private void OnDestroy()
        {
            _questService.OnQuestFinished -= SceneQuestFinished;
        }

        protected override bool IsValidContext(SceneLoaderContext sceneLoaderContext)
        {
            return base.IsValidContext(sceneLoaderContext) && sceneLoaderContext.ContextData is QuestObject;
        }

        protected override void LoadContext(SceneLoaderContext sceneLoaderContext)
        {
            var questObject = sceneLoaderContext.ContextData as QuestObject;

            if (isDebugLogEnabled)
            {
                Debug.Log($"Loading quest {questObject.Name} with id {questObject.Id}");
            }

            _questService.Init(questObject);
        }

        private void SceneQuestFinished(QuestObject quest)
        {
            var questScene = gameObject.scene;
            SceneManager.UnloadSceneAsync(questScene);
        }
    }
}
