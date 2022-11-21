using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuestObject = Quest.ScriptableObjects.Quest;

namespace Core.ContextLoader
{
    public class ContextLoaderService : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadScene("Scenes/DebugMenuScene", LoadSceneMode.Additive);
        }

        public void LoadScene(string sceneName, LoadSceneMode loadSceneMode, object contextData = null)
        {
            TryLoadScene(sceneName, loadSceneMode, contextData);
        }

        public bool TryLoadScene(string sceneName, LoadSceneMode loadSceneMode, object contextData = null)
        {
            var parameters = new LoadSceneParameters(loadSceneMode);
            var loadedScene = SceneManager.LoadScene(sceneName, parameters);

            var loaderContext = new SceneLoaderContext()
            {
                SceneName = loadedScene.name,
                SceneIndex = loadedScene.buildIndex,
                SceneMode = loadSceneMode,
                ContextData = contextData
            };

            SceneManager.sceneLoaded += SceneLoaded;

            void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
            {
                SceneManager.sceneLoaded -= SceneLoaded;

                foreach (var gameObject in scene.GetRootGameObjects())
                {
                    if (gameObject.TryGetComponent<ContextLoader>(out var contextLoader))
                    {
                        contextLoader.TryLoadContext(loaderContext);
                    }

                    if (gameObject.TryGetComponent<Camera>(out var camera))
                    {
                        gameObject.SetActive(false);
                    }
                }
            }

            return true;
        }
    }
}
