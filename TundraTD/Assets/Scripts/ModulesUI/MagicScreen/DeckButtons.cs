using System;
using System.Collections.Specialized;
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

            foreach (BasicElement element in Enum.GetValues(typeof(BasicElement)))
                PlayerDeck.ElementsInDeck[element] = 0;
            
            for (int i = 0; i < deckButtons.Length; i++)
            {
                if (i < PlayerDeck.DeckElements.Count)
                {
                    PlayerDeck.ElementsInDeck[elements[i]] += 1;
                    deckButtons[i].UpdateButtonElement(elements[i]);
                }
                else
                {
                    PlayerDeck.ElementsInDeck[BasicElement.None] += 1;
                    deckButtons[i].UpdateButtonElement(BasicElement.None);
                }
            }

            foreach (var pair in PlayerDeck.ElementsInDeck)
            {
                if (pair.Value < 3) continue;
                
                PlayerDeck.CurrentMostElement = pair.Key;
                return;
            }
        }
    }
}