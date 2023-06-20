using Project.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Image
{
    public class ImageNode : MonoBehaviour
    {
        private int _number;
        private ImageView _imageView;
        private static int _selected = -1;

        public static int Selected => _selected;
        
        public void Setup(int number, ImageView imageView)
        {
            _number = number;
            _imageView = imageView;
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (!SceneLoader.Instance) return;

            _selected = _number;
            
            SceneLoader.Instance.LoadSceneOnTop(_imageView.ViewSceneName);
        }
    }
}