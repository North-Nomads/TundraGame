using Spells;
using UnityEngine;

namespace UI.MagicScreen
{
    public class SpellCaster : MonoBehaviour
    {
        [SerializeField] private UpperButtonElements buttonsHolder;
        private readonly BasicElement[] _holdSpells = new BasicElement[5]; 
        public void OnButtonClick()
        {
            for (int i = 0; i < 5; i++)
            {
                _holdSpells[i] = buttonsHolder.ElementScripts[i].Element;
                buttonsHolder.ElementScripts[i].Clear();
            }
            Grimoire.TurnElementsIntoSpell(_holdSpells);
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