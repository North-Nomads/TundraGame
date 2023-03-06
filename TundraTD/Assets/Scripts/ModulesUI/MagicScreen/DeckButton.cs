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
        [SerializeField] private Image borderHolder;

        private BasicElement _element;
        private Sprite _nullElementSprite;
        private Sprite _defaultBorder;
        private Sprite _selectedBorder;

        private void Start()
        {
            _nullElementSprite = Resources.Load<Sprite>("Elements/BorderCircle");
            _defaultBorder = Resources.Load<Sprite>("Elements/BorderDefaultCircle");
            _selectedBorder = Resources.Load<Sprite>("Elements/BorderSelectedCircle");
        }

        public void OnButtonClick()
        {
            int index = Array.IndexOf(buttonsHolder.deckButtons, this);
            if (index < PlayerDeck.DeckElements.Count) 
                PlayerDeck.DeckElements.RemoveAt(index);
            _element = BasicElement.None; 
        }

        public void UpdateButtonElement(BasicElement element)
        {
            var sprite = PlayerDeck.ElementIcons[element];
            _element = element;
            
            if (sprite is null)
                iconHolder.sprite = _nullElementSprite;
            else
                iconHolder.sprite = sprite;
        }

        public void SetBorderSelection()
        {
            borderHolder.sprite = _selectedBorder;
        }

        public void ResetBorderSelection()
        {
            borderHolder.sprite = _defaultBorder;
        }
    }
}