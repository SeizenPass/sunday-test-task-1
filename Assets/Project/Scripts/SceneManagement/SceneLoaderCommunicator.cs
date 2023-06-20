using UnityEngine;

namespace Project.Scripts.SceneManagement
{
    public class SceneLoaderCommunicator : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            if (!SceneLoader.Instance)
            {
                Debug.LogError($"No {nameof(SceneLoader)} located");
                return;
            }
            
            SceneLoader.Instance.LoadScene(sceneName);
        }
    }
}