using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string loadingSceneName;
        
        [Range(0.001f, 10f)]
        [SerializeField] private float loadTime = 2;

        public static SceneLoader Instance { get; private set; }

        public float LastLoadActivationTime { get; private set; }
        public float LoadTime => loadTime;

        public Stack<string> _scenesStack = new Stack<string>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName)
        {
            LoadLoadingScene();
            StartCoroutine(WaitProcess(Callback));
            
            void Callback()
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        
        private IEnumerator WaitProcess(Action callback)
        {
            yield return new WaitForSeconds(loadTime);

            callback?.Invoke();
        }

        public void LoadSceneOnTop(string sceneName)
        {
            LoadLoadingScene(LoadSceneMode.Additive);
            StartCoroutine(WaitProcess(Callback));
            
            void Callback()
            {
                SceneManager.UnloadSceneAsync(loadingSceneName).completed += _ =>
                {
                    SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                    _scenesStack.Push(sceneName);
                };
            }
        }

        public void GoBack()
        {
            if (_scenesStack.Count <= 0) return;
            SceneManager.UnloadSceneAsync(_scenesStack.Pop());
            LoadLoadingScene(LoadSceneMode.Additive);
            StartCoroutine(WaitProcess(Callback));
            
            void Callback()
            {
                SceneManager.UnloadSceneAsync(loadingSceneName);
            }
        }

        private void LoadLoadingScene(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            LastLoadActivationTime = Time.time;
            SceneManager.LoadScene(loadingSceneName, loadSceneMode);
        }
    }
}