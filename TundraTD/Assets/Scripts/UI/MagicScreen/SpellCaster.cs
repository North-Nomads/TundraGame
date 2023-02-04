using Spells;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace UI.MagicScreen
{
    public class SpellCaster : MonoBehaviour
    {
        [SerializeField] private UpperButtonElements buttonsHolder;
        readonly BasicElement[] HoldSpells = new BasicElement[5];

        public void OnButtonClick()
        {
            for (int i = 0; i < 5; i++)
            {
                HoldSpells[i] = buttonsHolder.ElementScripts[i].Element;
                buttonsHolder.ElementScripts[i].Clear();
            }
            Grimoire.TurnElementsIntoSpell(HoldSpells);
        }
    }
}