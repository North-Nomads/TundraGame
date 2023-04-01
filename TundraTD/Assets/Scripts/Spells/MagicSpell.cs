using Assets.Scripts.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Spells
{
    public abstract class MagicSpell : MonoBehaviour
    {
        private static Dictionary<BasicElement, MagicSpell> _prefabs;

        public static Dictionary<BasicElement, AdditionalSpellEffect> AdditionalSpellEffects { get; set; }

        public AdditionalSpellEffect SpellEffect { get; private set; }

        public abstract BasicElement Element { get; }

        static MagicSpell()
        {
            
        }

        public abstract void ExecuteSpell(RaycastHit hitInfo);

        public static MagicSpell Instantiate(BasicElement basis, BasicElement addition = BasicElement.None)
        {
            if (_prefabs.ContainsKey(basis))
            {
                var spell = Instantiate(_prefabs[basis]);
                if (addition != BasicElement.None && AdditionalSpellEffects.ContainsKey(addition))
                {
                    spell.SpellEffect = AdditionalSpellEffects[addition];
                }
                return spell;
            }
            return null;
        }

        public static void SetPrefabs(MagicSpell[] prefabs)
        {
            _prefabs = prefabs.Where(x => x != null).ToDictionary(x => x.Element, y => y);
        }
    }
}