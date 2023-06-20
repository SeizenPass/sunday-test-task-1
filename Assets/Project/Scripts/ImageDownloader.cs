using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Scripts
{
    public class ImageDownloader : MonoBehaviour
    {
        [SerializeField] private int maxNumber = 66;
        [SerializeField] private int startAt = 1;
        [SerializeField] private string baseUri;

        public Action<int, Texture2D, bool> ImageDownloadedEvent;
        public bool ReachedMax => _currentNumber >= maxNumber;

        private int _currentNumber;

        private void Awake()
        {
            _currentNumber = startAt;
        }

        public int GetNewImage()
        {
            if (ReachedMax) return -1;

            StartCoroutine(GetTexture(_currentNumber));

            return _currentNumber++;
        }
        
        public IEnumerator GetTexture(int number)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture($"{baseUri}{number}.jpg"))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                    ImageDownloadedEvent?.Invoke(number, null, false);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    ImageDownloadedEvent?.Invoke(number, texture, true);
                }
            }
        }
    }
}