using UnityEngine;
using UnityEngine.UI;

namespace Mobs
{
    public class PortalInfoCanvas : MonoBehaviour
    {
        [SerializeField] private Image[] mobPortraits;
        [SerializeField] private Image[] borderImages;
        [SerializeField] private Sprite border;
        [SerializeField] private Sprite closedBorder;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void LoadImagesInCards(Sprite[] images)
        {
            for (int i = 0; i < borderImages.Length; i++)
            {
                if (images[i] == null)
                {
                    borderImages[i].sprite = closedBorder;
                }
                else
                {
                    borderImages[i].sprite = border;
                    mobPortraits[i].sprite = images[i];
                }
            }
        }
    }
}