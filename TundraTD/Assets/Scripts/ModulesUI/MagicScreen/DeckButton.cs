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

        public BasicElement Element { get; set; }
        public Image ElementIcon { get; set; }

        private void Start()
        {
            ElementIcon = GetComponent<Image>();
        }

        public void OnButtonClick()
        {
            int index = Array.IndexOf(buttonsHolder.deckButtons, this);
            if (index < PlayerDeck.DeckElements.Count) 
                PlayerDeck.DeckElements.RemoveAt(index);
        }

        public void UpdateButtonElement(BasicElement element)
        {
            ElementIcon.sprite = PlayerDeck.ElementIcons[element];
            Element = element;
        }
    }
}