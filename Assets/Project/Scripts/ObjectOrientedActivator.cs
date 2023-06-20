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
                SwitchTo(LayoutMode.Vertical);
            }
            else
            {
                _currentLayoutMode = LayoutMode.Horizontal;
                SwitchTo(LayoutMode.Horizontal);
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
                
                SwitchTo(LayoutMode.Vertical);
            }
            else
            {
                if (_currentLayoutMode == LayoutMode.Horizontal) return;
                
                _currentLayoutMode = LayoutMode.Horizontal;

                SwitchTo(LayoutMode.Horizontal);
            }
        }

        private void SwitchTo(LayoutMode layoutMode)
        {
            SetElementsState(layoutMode == LayoutMode.Horizontal, LayoutMode.Horizontal);
            SetElementsState(layoutMode == LayoutMode.Vertical, LayoutMode.Vertical);
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