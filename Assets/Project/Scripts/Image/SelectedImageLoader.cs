using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Image
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