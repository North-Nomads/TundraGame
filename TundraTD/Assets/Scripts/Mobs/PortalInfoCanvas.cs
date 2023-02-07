using UnityEngine;
using UnityEngine.UI;

namespace Mobs
{
    public class PortalInfoCanvas : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup mobCardsContent;
        private Image[] _mobCards;

        private void Start()
        {
            _mobCards = mobCardsContent.GetComponentsInChildren<Image>();
            gameObject.SetActive(false);
        }

        public void LoadImagesInCards(Sprite[] images)
        {
            for (int i = 0; i < _mobCards.Length; i++)
            {
                if (images[i] == null)
                {
                    _mobCards[i].gameObject.SetActive(false);
                }
                else
                {
                    _mobCards[i].gameObject.SetActive(true);
                    _mobCards[i].sprite = images[i];
                }
            }
        }
    }
}