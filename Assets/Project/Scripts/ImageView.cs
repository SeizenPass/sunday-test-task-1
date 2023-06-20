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
        [SerializeField] private RawImage imageNodePrefab;
        [SerializeField] private ImageDownloader imageDownloader;


        private Dictionary<int, RawImage> _images;
        private const float LoadPoint = 0.10f;

        private void Awake()
        {
            _images = new Dictionary<int, RawImage>();
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

            _images[number].texture = texture2D;
        }

        private void Listen(Vector2 val)
        {
            Debug.Log(val);
            if (val.y <= LoadPoint)
            {
                scrollRect.onValueChanged.RemoveListener(Listen);
                StartCoroutine(ImageAddition());
            }
        }

        private void OnDestroy()
        {
            imageDownloader.ImageDownloadedEvent -= SetImage;
            scrollRect.onValueChanged.RemoveListener(Listen);
        }
    }
}
