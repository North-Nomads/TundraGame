using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spells;
using UnityEngine;

namespace Assets.Spells.SpellClasses
{
    [Spell(BasicElement.Fire, "Meteor", "Casts a fire meteor on heads of your enemies.")]
    public class FireballSpell : MagicSpell
    {
        private const float HitDelay = 0.5f;
        private float currentHitTime;

        /// <summary>
        /// The radius of the hit area
        /// </summary>
        public float HitDamageRadius { get; set; } = 5f;

        /// <summary>
        /// The damage of the hit epicenter.
        /// </summary>
        [MultiplictableProperty(BasicElement.Earth, 1.35f)]
        public float HitDamageValue { get; set; } = 100f;

        /// <summary>
        /// Duration of the burn effect.
        /// </summary>
        [IncreasableProperty(BasicElement.Air, 5f)]
        public float BurnDuration { get; set; } = 5f;

        /// <summary>
        /// Damage of the burn effect.
        /// </summary>
        [MultiplictableProperty(BasicElement.Fire, 1.25f)]
        [MultiplictableProperty(BasicElement.Water, 0.75f)]
        public float BurnDamage { get; set; } = 20f;

        /// <summary>
        /// Value of the slowness effect.
        /// </summary>
        public float SlownessValue { get; set; } = 0.3f;

        /// <summary>
        /// Duration of the slowness effect.
        /// </summary>
        [IncreasableProperty(BasicElement.Lightning, 2f)]
        public float SlownessDuration { get; set; }

        public override void ExecuteSpell()
        {
            Debug.Log($"Meteor cast. Hit damage: {HitDamageValue}; Burn duration: {BurnDuration}; Burn damage: {BurnDamage}; Slowness duration: {SlownessDuration}.");
            throw new NotImplementedException();
        }

        private void Update()
        {
            currentHitTime += Time.deltaTime;
            if (currentHitTime > HitDelay)
            {
                // ..Here should be hit handler
            }
        }
    }
}
