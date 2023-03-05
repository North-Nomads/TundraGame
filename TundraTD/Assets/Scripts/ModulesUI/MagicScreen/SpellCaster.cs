using System.Linq;
using Spells;
using UnityEngine;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// A button orders grimoire to generate spell of current deck 
    /// </summary>
    public class SpellCaster : MonoBehaviour
    {
        public void OnButtonClick()
        {
            Grimoire.TurnElementsIntoSpell(PlayerDeck.DeckElements.ToList(), Vector3.zero);
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