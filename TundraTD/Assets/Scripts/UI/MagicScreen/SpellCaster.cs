using System.Linq;
using Spells;
using UnityEngine;

namespace UI.MagicScreen
{
    public class SpellCaster : MonoBehaviour
    {
        [SerializeField] private DeckButtons deckHolder;
        private readonly BasicElement[] _holdSpells = new BasicElement[5]; 
        public void OnButtonClick()
        {
            Grimoire.TurnElementsIntoSpell(PlayerDeck.DeckElements.ToList());
            PlayerDeck.DeckElements.Clear();
        }

        //For debug purposes only. Remove it before pulling
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                OnButtonClick();
            }
        }

    }
}