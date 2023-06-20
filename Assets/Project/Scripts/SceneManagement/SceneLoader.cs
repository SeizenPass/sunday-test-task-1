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

        private Stack<string> _scenesStack = new();

        private List<string> _path = new();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _path.Add(SceneManager.GetActiveScene().name);
        }

        public void LoadScene(string sceneName)
        {
            LoadLoadingScene();
            StartCoroutine(WaitProcess(Callback));
            
            void Callback()
            {
                SceneManager.LoadScene(sceneName);
                _path.Add(sceneName);
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
            switch (_scenesStack.Count)
            {
                case <= 0 when _path.Count <= 1:
                    return;
                case <= 0:
                {
                    var targetIndex = _path.Count - 1;
                    _path.RemoveAt(targetIndex);
                    LoadLoadingScene();
                    StartCoroutine(WaitProcess(Callback));

                    void Callback()
                    {
                        SceneManager.LoadScene(_path[^1]);
                    }

                    break;
                }
                default:
                {
                    LoadLoadingScene(LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(_scenesStack.Pop());
                    StartCoroutine(WaitProcess(Callback));
            
                    void Callback()
                    {
                        SceneManager.UnloadSceneAsync(loadingSceneName);
                    }

                    break;
                }
            }
        }

        private void LoadLoadingScene(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            LastLoadActivationTime = Time.time;
            SceneManager.LoadScene(loadingSceneName, loadSceneMode);
        }
    }
}