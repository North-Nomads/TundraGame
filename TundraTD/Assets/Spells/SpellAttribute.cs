using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Spells
{
    /// <summary>
    /// Used to detect if the class is a magic spell.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SpellAttribute : Attribute
    {
        /// <summary>
        /// A core element used to create a spell.
        /// </summary>
        public BasicElement CoreElement { get; set; }
        /// <summary>
        /// A description of a spell.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The name of the spell.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Creates the new instance of a <see cref="SpellAttribute"/> using all provided data.
        /// </summary>
        /// <param name="coreElement"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public SpellAttribute(BasicElement coreElement, string name, string description)
        {
            CoreElement = coreElement;
            Name = name;
            Description = description;
        }
    }
}
