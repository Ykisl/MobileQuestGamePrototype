using Core.ContextLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace InitScene
{
    public class InitSceneLoaderService : MonoBehaviour
    {
        [SerializeField] private string startSceneName;

        [Inject] private ContextLoaderService _contextLoaderService;

        private void Start()
        {
            _contextLoaderService.LoadScene(startSceneName, LoadSceneMode.Additive);
        }
    }
}