using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Spells
{
    public class SpellAttribute : Attribute
    {
        public BasicElement CoreElement { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public SpellAttribute(BasicElement coreElement, string name, string description)
        {
            CoreElement = coreElement;
            Name = name;
            Description = description;
        }
    }
}
