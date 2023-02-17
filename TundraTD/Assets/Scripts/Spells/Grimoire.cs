using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Spells
{
    /// <summary>
    /// A helper class to build spells from elements.
    /// </summary>
    public static class Grimoire
    {
        private static readonly Dictionary<BasicElement, Type> _spellTypes;

        // HACK: temporary solution to avoid errors
        public static MagicSpell[] SpellInitializers { get; set; }

        static Grimoire()
        {
            _spellTypes = (from type in Assembly.GetExecutingAssembly().GetTypes()
                           where typeof(MagicSpell).IsAssignableFrom(type) && !type.IsAbstract
                           let element = type.GetCustomAttribute<SpellAttribute>()
                           where element != null
                           select (element.CoreElement, type)).ToDictionary(x => x.CoreElement, y => y.type);
        }

        /// <summary>
        /// Turns elements into a spell if it's available.
        /// </summary>
        /// <param name="elements">Elements list.</param>
        /// <returns>Created spell which is ready to cast.</returns>
        public static MagicSpell TurnElementsIntoSpell(BasicElement[] elements)
        {
            BasicElement? mostElement = elements.GroupBy(x => x).FirstOrDefault(x => x.Count() >= 3)?.Key;
            Array.Sort(elements);
            int startMostIndex = Array.IndexOf(elements, mostElement);
            var remElements = elements.Where((x, i) => x != mostElement || i >= startMostIndex + 3);
            if (mostElement.HasValue && _spellTypes.TryGetValue(mostElement.Value, out Type spellType))
            {
                var spellObject = SpellInitializers[(int)Math.Log((int)mostElement, 2)];
                MagicSpell spell = spellObject.GetComponent(spellType) as MagicSpell;
                foreach (var prop in spellType.GetProperties())
                {
                    foreach (var attr in prop.GetCustomAttributes<UpgradeablePropertyAttribute>(true))
                    {
                        foreach (var element in remElements)
                        {
                            attr.TryUpgradeProperty(element, prop, spell);
                        }
                    }
                }
                spell.InstantiateSpellExecution();
                return spell;
            }
            return null;
        }
    }
}