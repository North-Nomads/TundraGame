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
        
        public abstract void ExecuteSpell(RaycastHit hitInfo);

        public static MagicSpell InstantiateSpellPrefab(BasicElement basis, BasicElement addition = BasicElement.None)
        {
            if (!_prefabs.ContainsKey(basis))
                return null;
            var spell = Instantiate(_prefabs[basis]);
            if (addition != BasicElement.None && AdditionalSpellEffects.ContainsKey(addition))
            {
                spell.SpellEffect = AdditionalSpellEffects[addition];
            }
            return spell;
        }

        /// <summary>
        /// Sets spell prefabs into the internal dictionary based on their basic elements.
        /// </summary>
        /// <param name="prefabs">Prefabs array that can be set up in the Editor.</param>
        public static void SetSpellPrefabs(MagicSpell[] prefabs)
        {
            _prefabs = prefabs.Where(x => x != null).ToDictionary(x => x.Element, y => y);
        }
    }
}