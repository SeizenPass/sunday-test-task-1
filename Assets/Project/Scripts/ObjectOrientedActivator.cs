using System;
using UnityEngine;

namespace Project.Scripts
{
    public class ObjectOrientedActivator : MonoBehaviour
    {
        [SerializeField] private GameObject[] horizontals, verticals;

        private LayoutMode _currentLayoutMode;
        private bool _initialCheck;

        private void Start()
        {
            if (Screen.orientation == ScreenOrientation.Portrait ||
                Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                _currentLayoutMode = LayoutMode.Vertical;
                SetElementsState(true, LayoutMode.Vertical);
                SetElementsState(false, LayoutMode.Horizontal);
            }
            else
            {
                _currentLayoutMode = LayoutMode.Horizontal;
                SetElementsState(false, LayoutMode.Vertical);
                SetElementsState(true, LayoutMode.Horizontal);
            }
        }

        private void Update()
        {
            CheckForChange();
        }

        private void CheckForChange()
        {
            if (Screen.orientation == ScreenOrientation.Portrait ||
                Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                if (_currentLayoutMode == LayoutMode.Vertical) return;
                
                _currentLayoutMode = LayoutMode.Vertical;
                
                SetElementsState(true, LayoutMode.Vertical);
                SetElementsState(false, LayoutMode.Horizontal);
            }
            else
            {
                if (_currentLayoutMode == LayoutMode.Horizontal) return;
                
                _currentLayoutMode = LayoutMode.Horizontal;

                SetElementsState(false, LayoutMode.Vertical);
                SetElementsState(true, LayoutMode.Horizontal);
            }
        }

        private void SetElementsState(bool elementsActive, LayoutMode layoutMode)
        {
            if (layoutMode == LayoutMode.Horizontal)
            {
                foreach (var h in horizontals)
                {
                    h.SetActive(elementsActive);
                }
            }
            else
            {
                foreach (var h in verticals)
                {
                    h.SetActive(elementsActive);
                }
            }
        }

        private enum LayoutMode
        {
            Horizontal, Vertical
        }
    }
}