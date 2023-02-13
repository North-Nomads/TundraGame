using Spells;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MagicScreen
{
    /// <summary>
    /// A filler that connects the deck and the elements
    /// </summary>
    public class DeckFiller : MonoBehaviour
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private UpperButtonElements buttonsHolder;
        [SerializeField] private Image elementIcon;

        public void OnButtonClick()
        {
            var empty = System.Array.Find(buttonsHolder.ElementScripts, x => x.Element == BasicElement.None);
            if (empty == null) return;
            
            empty.ElementIcon.sprite = elementIcon.sprite;
            empty.Element = element;
        }
    }
}