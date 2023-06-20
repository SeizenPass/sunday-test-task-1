using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class ImageView : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject content;
        [SerializeField] private ImageNode imageNodePrefab;
        [SerializeField] private ImageDownloader imageDownloader;
        [SerializeField] private string viewSceneName;

        public string ViewSceneName => viewSceneName;

        private Dictionary<int, ImageNode> _images;
        private const float LoadPoint = 0.10f;

        private void Awake()
        {
            _images = new Dictionary<int, ImageNode>();
        }

        private void Start()
        {
            imageDownloader.ImageDownloadedEvent += SetImage;
            
            StartCoroutine(ImageAddition());
        }

        private IEnumerator ImageAddition()
        {
            while (scrollRect.verticalNormalizedPosition <= LoadPoint)
            {
                var number = imageDownloader.GetNewImage();
                
                if (number == -1) break;
                
                var createdImage = Instantiate(imageNodePrefab, content.transform);
                _images[number] = createdImage;
                
                createdImage.Setup(number, this);
                
                yield return null;
            }
            
            scrollRect.onValueChanged.AddListener(Listen);
        }

        private void SetImage(int number, Texture2D texture2D, bool success)
        {
            if (!success)
            {
                Debug.LogWarning("Couldn't load the image. Destroying placeholder.");
                Destroy(_images[number].gameObject);
                return;
            }

            _images[number].GetComponent<RawImage>().texture = texture2D;
        }

        private void Listen(Vector2 val)
        {
            if (!(val.y <= LoadPoint)) return;
            scrollRect.onValueChanged.RemoveListener(Listen);
            StartCoroutine(ImageAddition());
        }

        private void OnDestroy()
        {
            imageDownloader.ImageDownloadedEvent -= SetImage;
            scrollRect.onValueChanged.RemoveListener(Listen);
        }
    }
}
