using System;
using Spells;
using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// A button that shows a part of player's current deck
    /// </summary>
    public class DeckButton : MonoBehaviour
    {
        [SerializeField] private DeckButtons buttonsHolder;
        [SerializeField] private Image iconHolder;
        
        private Sprite _nullElementSprite;

        private void Start()
        {
            _nullElementSprite = Resources.Load<Sprite>("Elements/BorderCircle");
        }

        public void OnButtonClick()
        {
            int index = Array.IndexOf(buttonsHolder.deckButtons, this);
            if (index < PlayerDeck.DeckElements.Count) 
                PlayerDeck.DeckElements.RemoveAt(index);
        }

        public void UpdateButtonElement(BasicElement element)
        {
            var sprite = PlayerDeck.ElementIcons[element];
            
            if (sprite is null)
                iconHolder.sprite = _nullElementSprite;
            else
                iconHolder.sprite = sprite;
        }
    }
}