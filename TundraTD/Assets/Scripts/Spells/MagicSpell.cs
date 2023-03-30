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
        private static readonly Dictionary<BasicElement, Type> _spellTypes = new Dictionary<BasicElement, Type>();

        private static readonly Dictionary<BasicElement, AdditionalSpellEffect> _additionalSpellEffects;

        public AdditionalSpellEffect SpellEffect { get; private set; }

        static MagicSpell()
        {
            _spellTypes = (from type in Assembly.GetExecutingAssembly().GetTypes()
                           where typeof(MagicSpell).IsAssignableFrom(type) && !type.IsAbstract
                           let element = type.GetCustomAttribute<SpellAttribute>()
                           where element != null
                           select (element.CoreElement, type)).ToDictionary(x => x.CoreElement, y => y.type);
            // TODO: print here the path to load additional effects.
            _additionalSpellEffects = Resources.LoadAll<AdditionalSpellEffect>("path/to/load").ToDictionary(x => x.Element, y => y);
        }

        public abstract void ExecuteSpell(RaycastHit hitInfo);

        public static MagicSpell Instantiate(BasicElement basis, BasicElement addition = BasicElement.None)
        {
            if (_spellTypes.ContainsKey(basis))
            {
                var spell = (MagicSpell)Activator.CreateInstance(_spellTypes[basis]);
                if (_additionalSpellEffects.ContainsKey(addition))
                {
                    spell.SpellEffect = _additionalSpellEffects[addition];
                }
                return spell;
            }
            return null;
        }
    }
}