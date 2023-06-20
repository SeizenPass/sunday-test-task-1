using System;
using System.Collections;
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

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName)
        {
            LastLoadActivationTime = Time.time;
            SceneManager.LoadScene(loadingSceneName);

            IEnumerator WaitProcess()
            {
                yield return new WaitForSeconds(loadTime);

                SceneManager.LoadScene(sceneName);
            }

            StartCoroutine(WaitProcess());
        }
    }
}