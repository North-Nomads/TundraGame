using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Spells
{
    public abstract class Wizard : MonoBehaviour
    {
        [SerializeField]
        protected MagicSpell PrefabToInstantiate;

        public virtual MagicSpell CastSpell(Type spellType, IEnumerable<BasicElement> remElements)
        {
            var spell = InstantiateSpellObject();
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

        public virtual MagicSpell InstantiateSpellObject() => Instantiate(PrefabToInstantiate);
    }
}
