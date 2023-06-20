using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class SelectedImageLoader : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;

        private void Start()
        {
            rawImage.texture = ImageDownloader.GetLoadedTexture(ImageNode.Selected);
        }
    }
}