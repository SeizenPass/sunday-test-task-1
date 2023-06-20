using UnityEngine;

namespace Project.Scripts.SceneManagement
{
    public class SceneLoaderCommunicator : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            if (!Valid) return;
            SceneLoader.Instance.LoadScene(sceneName);
        }

        public void LoadSceneOnTop(string sceneName)
        {
            if (!Valid) return; 
            SceneLoader.Instance.LoadSceneOnTop(sceneName);
        }

        private void GoBack()
        {
            if (!Valid) return;
            SceneLoader.Instance.GoBack();
        }

        private bool Valid => SceneLoader.Instance;
    }
}