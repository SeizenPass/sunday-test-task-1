using Project.Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI loadingText;

        private SceneLoader _sceneLoader;
        private void Start()
        {
            _sceneLoader = SceneLoader.Instance;
            if (!_sceneLoader) enabled = false;

        }

        private void Update()
        {
            float val = _sceneLoader.LastLoadActivationTime + _sceneLoader.LoadTime - Time.time;
            val /= _sceneLoader.LoadTime;
            val = 1 - val;

            slider.value = val;
            loadingText.text = $"{Mathf.Clamp(Mathf.Round(val * 100f), 0f, 100f)}%";
        }
    }
}