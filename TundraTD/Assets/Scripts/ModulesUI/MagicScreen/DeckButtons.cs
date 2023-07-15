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
        }
    }
}