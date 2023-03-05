using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Spells
{
    /// <summary>
    /// A helper class to build spells from elements.
    /// </summary>
    public static class Grimoire
    {
        private static readonly Dictionary<BasicElement, Type> SpellTypes;
        private static bool _isCastingSpell = false;
        public static MagicSpell[] SpellInitializers { get; set; }
        public static bool IsCastingSpell => _isCastingSpell;

        static Grimoire()
        {
            SpellTypes = (from type in Assembly.GetExecutingAssembly().GetTypes()
                           where typeof(MagicSpell).IsAssignableFrom(type) && !type.IsAbstract
                           let element = type.GetCustomAttribute<SpellAttribute>()
                           where element != null
                           select (element.CoreElement, type)).ToDictionary(x => x.CoreElement, y => y.type);
        }

        /// <summary>
        /// Turns elements into a spell if it's available.
        /// </summary>
        /// <param name="elements">Elements list.</param>
        /// <param name="hitInfo"></param>
        /// <returns>Created spell which is ready to cast.</returns>
        public static void TurnElementsIntoSpell(List<BasicElement> elements, RaycastHit hitInfo)
        {
            BasicElement? mostElement = elements.GroupBy(x => x).FirstOrDefault(x => x.Count() >= 3)?.Key;
            
            if (!mostElement.HasValue) return;

            elements.Sort();
            int startMostIndex = elements.IndexOf(mostElement.Value); 
            var remainingElements = elements.Where((x, i) => x != mostElement || i >= startMostIndex + 3);

            if (!SpellTypes.TryGetValue(mostElement.Value, out Type spellType)) return;

            var spellObject = SpellInitializers[(int)Math.Log((int)mostElement, 2)];
            
            // don't call any spell if this spell was not implemented (for alpha purposes only)
            // TODO: Remove
            if (spellObject is null) return;

            var spell = Object.Instantiate(spellObject);
            
            foreach (var prop in spellType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
            foreach (var attr in prop.GetCustomAttributes<UpgradeablePropertyAttribute>(true))
            foreach (var element in remainingElements)
                attr.TryUpgradeProperty(element, prop, spell);
            
            spell.SpellCameraLock += HandleSpellCameraLock;
            spell.ExecuteSpell(hitInfo);
        }

        private static void HandleSpellCameraLock(object sender, bool value)
        {
            _isCastingSpell = value;
        }
    }
}