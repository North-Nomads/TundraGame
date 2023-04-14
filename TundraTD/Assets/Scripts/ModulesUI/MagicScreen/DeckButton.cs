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

        private Sprite _nullElementSprite;
        private Sprite _defaultBorder;
        private Sprite _selectedBorder;
        private BasicElement _element;

        public BasicElement Element => _element;
        
        private void Start()
        {
            _nullElementSprite = Resources.Load<Sprite>("Elements/None");
            _defaultBorder = Resources.Load<Sprite>("Elements/BorderDefaultCircle");
            _selectedBorder = Resources.Load<Sprite>("Elements/BorderSelectedCircle");
        }

        public void OnButtonClick()
        {
            int index = Array.IndexOf(buttonsHolder.deckButtons, this);
            if (index < PlayerDeck.DeckElements.Count)
            {
                PlayerDeck.ElementCharges[_element].ReturnCharge();
                PlayerDeck.DeckElements.RemoveAt(index);
            }
        }

        public void UpdateButtonElement(BasicElement element)
        {
            var sprite = PlayerDeck.ElementIcons[element];

            iconHolder.sprite = sprite != null ? sprite : _nullElementSprite;

            _element = element;
        }

        public void SetBorderSelection()
        {
            borderHolder.sprite = _selectedBorder;
        }

        public void RemoveBorderSelection()
        {
            borderHolder.sprite = _defaultBorder;
        }
    }
}