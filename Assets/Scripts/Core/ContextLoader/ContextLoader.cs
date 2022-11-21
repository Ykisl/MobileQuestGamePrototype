using Quest.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.ContextLoader
{
    public class ContextLoader : MonoBehaviour
    {
        [SerializeField] protected bool isDebugLogEnabled;

        public bool TryLoadContext(SceneLoaderContext sceneLoaderContext)
        {
            if (!IsValidContext(sceneLoaderContext))
            {
                return false;
            }

            if (isDebugLogEnabled)
            {
                Debug.Log($"Loading {sceneLoaderContext.SceneName} scene with context");
            }

            LoadContext(sceneLoaderContext);
            return true;
        }

        protected virtual bool IsValidContext(SceneLoaderContext sceneLoaderContext)
        {
            return sceneLoaderContext != null;
        }

        protected virtual void LoadContext(SceneLoaderContext sceneLoaderContext)
        {
        }
    }
}
