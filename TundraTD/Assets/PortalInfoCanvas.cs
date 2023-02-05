using UnityEngine;
using UnityEngine.UI;

public class PortalInfoCanvas : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup mobCardsContent;
    private Image[] _mobCards;
    
    private void Start()
    {
        _mobCards = mobCardsContent.GetComponentsInChildren<Image>();
    }

    public void LoadImagesInCards(Sprite[] images)
    {
        for (int i = 0; i < _mobCards.Length; i++)
        {
            print(images);
            _mobCards[i].sprite = images[i];
        }
    }
}
