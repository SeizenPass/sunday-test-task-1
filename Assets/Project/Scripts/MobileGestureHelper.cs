using System;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts
{
    public class MobileGestureHelper : MonoBehaviour
    {
        public UnityEvent onGoBack;

        private static MobileGestureHelper _current;
        private MobileGestureHelper _prev;

        private void Start()
        {
            if (_current)
            {
                _prev = _current;
                _prev.enabled = false;
            }
            _current = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                onGoBack.Invoke();
            }
        }

        private void OnDestroy()
        {
            if (!_prev) return;
            _current = _prev;
            _current.enabled = true;
        }
    }
}