using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.ContextLoader 
{
    public class SceneLoaderContext
    {
        public string SceneName;
        public int SceneIndex;
        public LoadSceneMode SceneMode;
        public object ContextData;
    }
}
