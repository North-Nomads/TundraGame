using System;
using System.Collections.Specialized;
using System.Linq;
using Spells;
using UnityEngine;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// Class that manages deck buttons sprites
    /// </summary>
    public class DeckButtons : MonoBehaviour
    {
        [SerializeField] public DeckButton[] deckButtons;

        private void Start()
        {
            PlayerDeck.DeckElements.CollectionChanged += UpdateDeck;
        }

        private void OnDestroy()
        {
            PlayerDeck.DeckElements.CollectionChanged -= UpdateDeck;
        }

        private void UpdateDeck(object sender, NotifyCollectionChangedEventArgs e)
        {
            var elements = PlayerDeck.DeckElements;

            // Set deck icon depending on value of BasicElement or BasicElement.None 
            for (int i = 0; i < deckButtons.Length; i++)
                deckButtons[i].UpdateButtonElement(i < PlayerDeck.DeckElements.Count ? elements[i] : BasicElement.None);

            
            var mostElement = GetMostElement();
            
            // Remove all selections if most element is None
            if (mostElement == BasicElement.None)
            {
                foreach (var deckButton in deckButtons)
                    deckButton.RemoveBorderSelection();
            }
            else
            {
                // otherwise set the ones which are same as the most one
                foreach (var deckButton in deckButtons)
                {
                    if (deckButton.Element == mostElement)
                        deckButton.SetBorderSelection();
                    else
                        deckButton.RemoveBorderSelection();
                }
            }
            
            // Used to calculate the occurrences of each element
            BasicElement GetMostElement()
            {
                var occurrences = new[] { 0, 0, 0, 0, 0, 0 };
                foreach (var element in PlayerDeck.DeckElements)
                {
                    var index = (int)Math.Log((int)element, 2);
                    occurrences[index]++;
                    if (occurrences[index] >= 3)
                        return element;
                }

                return BasicElement.None;
            }
        }
    }
}