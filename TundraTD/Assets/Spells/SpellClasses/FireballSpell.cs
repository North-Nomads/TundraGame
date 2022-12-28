using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Spells.SpellClasses
{
    [Spell(BasicElement.Fire, "Fireball", "Casts a firestorm into heads of your enemies.")]
    public class FireballSpell : MagicSpell
    {
        public float HitDamageRadius { get; private set; }

        public float HitDamageValue { get; private set; }

        public float BurnDuration { get; private set; }

        public float BurnDamage { get; private set; }

        public override void ExecuteSpell()
        {
            throw new NotImplementedException();
        }
    }
}
