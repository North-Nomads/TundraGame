using UnityEngine;
using UnityEngine.UI;
using Spells;
using System.Linq;

namespace UI.MagicScreen
{
    /// <summary>
    /// A filler that connects the deck and the elements  
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class DeckFiller : MonoBehaviour
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private UpperButtonElements buttonsHolder;
        private Image _elementIcon;

        private void Start()
        {
            _elementIcon = GetComponent<Image>();
        }
        
        public void OnButtonClick()
        {
            var empty = buttonsHolder.ElementScripts.FirstOrDefault(x => x.Element == BasicElement.None);
            if (empty != null)
            {
                empty.ElementIcon.sprite = _elementIcon.sprite;
                empty.Element = element;
            }
        }
    }
}