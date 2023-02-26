using System.Collections.Specialized;
using Spells;
using UnityEngine;

namespace UI.MagicScreen
{
    public class DeckButtons : MonoBehaviour
    {
        [SerializeField] public DeckButton[] deckButtons;

        private void Start()
        {
            PlayerDeck.DeckElements.CollectionChanged += UpdateDeck;
        }

        private void UpdateDeck(object sender, NotifyCollectionChangedEventArgs e)
        {
            var elements = PlayerDeck.DeckElements;
            for (int i = 0; i < deckButtons.Length; i++)
            {
                if (i < PlayerDeck.DeckElements.Count)
                    deckButtons[i].UpdateButtonElement(elements[i]);
                else
                    deckButtons[i].UpdateButtonElement(BasicElement.None);
            }
        }
    }
}