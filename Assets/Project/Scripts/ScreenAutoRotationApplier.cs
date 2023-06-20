using UnityEngine;

namespace Project.Scripts
{
    public class ScreenAutoRotationApplier : MonoBehaviour
    {
        private void Start()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        private void OnDestroy()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}