using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spells.SpellClasses
{
    [Spell(BasicElement.Earth, "Spikes", "Creates a spikes in front of you to penetrate your enemies.")]
    public class SpikesSpell : MagicSpell
    {
        public float ApproachDelay { get; } = 0.2f;

        public float MaxDrawTime { get; set; } = 2f;

        [IncreasableProperty(BasicElement.Lightning, 10f)]
        public float MaxLength { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        public float CollisionDamage { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        public float FallDamage { get; set; } = 50f;

        [IncreasableProperty(BasicElement.Fire, 0.3f)]
        public float StunTime { get; set; } = 0.7f;

        [IncreasableProperty(BasicElement.Air, -0.7f)]
        public float SlownessTime { get; set; } = 2f;

        [IncreasableProperty(BasicElement.Water, -0.05f)]
        public float SlownessValue { get; set; } = 0.7f;

        public float Lifetime { get; set; } = 4f;

        public override void ExecuteSpell()
        {
            throw new NotImplementedException();
        }
    }
}