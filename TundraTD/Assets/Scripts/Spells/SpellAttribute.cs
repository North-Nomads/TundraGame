using System;

namespace Spells
{
    /// <summary>
    /// Used to detect if the class is a magic spell.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SpellAttribute : Attribute
    {
        public BasicElement CoreElement { get; }

        public string Description { get; }

        public string Name { get; }

        /// <summary>
        /// Creates the new instance of a <see cref="SpellAttribute"/> using all provided data.
        /// </summary>
        public SpellAttribute(BasicElement coreElement, string name, string description)
        {
            CoreElement = coreElement;
            Name = name;
            Description = description;
        }
    }
}