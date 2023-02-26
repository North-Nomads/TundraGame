using Spells;
using UnityEngine;

namespace UI.MagicScreen
{
    public class SpellCaster : MonoBehaviour
    {
        [SerializeField] private UpperButtonElements buttonsHolder;
        private readonly BasicElement[] HoldSpells = new BasicElement[5]; 
        public void OnButtonClick()
        {
            for (int i = 0; i < 5; i++)
            {
                HoldSpells[i] = buttonsHolder.ElementScripts[i].Element;
                buttonsHolder.ElementScripts[i].Clear();
            }
            Grimoire.TurnElementsIntoSpell(HoldSpells);
        }

        //For debug purpuses only
        //Remove it before pulling
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                OnButtonClick();
            }
        }

    }
}