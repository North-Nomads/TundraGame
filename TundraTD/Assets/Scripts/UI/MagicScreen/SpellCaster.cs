using System.Linq;
using Spells;
using UnityEngine;

namespace UI.MagicScreen
{
    /// <summary>
    /// A button orders grimoire to generate spell of current deck 
    /// </summary>
    public class SpellCaster : MonoBehaviour
    {
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