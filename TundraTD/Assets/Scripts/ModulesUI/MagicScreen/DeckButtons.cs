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
            PlayerDeck.CurrentMostElement = BasicElement.None;

            foreach (BasicElement element in Enum.GetValues(typeof(BasicElement)))
                PlayerDeck.ElementsQuantity[element] = 0;
            
            for (int i = 0; i < deckButtons.Length; i++)
            {
                if (i < PlayerDeck.DeckElements.Count)
                {
                    PlayerDeck.ElementsQuantity[elements[i]] += 1;
                    deckButtons[i].UpdateButtonElement(elements[i]);
                }
                else
                {
                    PlayerDeck.ElementsQuantity[BasicElement.None] += 1;
                    deckButtons[i].UpdateButtonElement(BasicElement.None);
                }
            }
            
            // Find most element in player deck
            foreach (var pair in PlayerDeck.ElementsQuantity)
            {
                Debug.Log(pair);                
                
                if (pair.Value < 3) continue;
                
                PlayerDeck.CurrentMostElement = pair.Key;
                
                // Iterate through elements collection and select or deselect borders  
                for (int i = 0; i < PlayerDeck.DeckElements.Count; i++)
                {
                    if (PlayerDeck.DeckElements[i] == PlayerDeck.CurrentMostElement)
                        deckButtons[i].SetBorderSelection();
                    else
                        deckButtons[i].ResetBorderSelection();
                }

                for (int i = PlayerDeck.DeckElements.Count; i < deckButtons.Length; i++)
                    deckButtons[i].ResetBorderSelection();
                
                
                return;
            }
        }
    }
}