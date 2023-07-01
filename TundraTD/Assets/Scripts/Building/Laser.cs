using System.Collections.Generic;
using Spells;
using Spells.SpellClasses;
using UnityEngine;


namespace Building
{
    /// <summary>
    /// Laser kills Meteor and is being disabled with lightning
    /// </summary>
    public class Laser : EnemyTower
    {
        [SerializeField] private float maxCooldownTime;
        [SerializeField] private float lightningScanRadius;

        private List<MeteorSpell> _meteors;
        private float _cooldownTime;

        protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
        {
            var spell = (MagicSpell)sender;

            // TODO: Extract this function in Lightning
            if (spell.Element == BasicElement.Lightning &&
                (e.HitInfo.point - transform.position).sqrMagnitude < lightningScanRadius * lightningScanRadius)
            {
                (spell as LightningBoltSpell).OverrideStrike(e.HitInfo.point, transform.position); 
                ResetCharge();
            }          
                

            if (spell.Element == (BasicElement.Fire | BasicElement.Earth))
            {
                Debug.Log("Meteorite entered the atmosphere");
                _meteors.Add(spell as MeteorSpell);
            }
        }

        protected override void ExecuteOnStart()
        {
            _meteors = new List<MeteorSpell>();
        }

        private void DestroyMeteorite()
        {
            _meteors[0].Explode();
            _meteors.Clear();
            ResetCharge();
        }

        public void ResetCharge()
        {
            _cooldownTime = maxCooldownTime;

        }

        private void FixedUpdate()
        {
            _cooldownTime -= Time.deltaTime;
            if (_cooldownTime > 0)
                return;

            if (_meteors.Count > 0)
            {
                Debug.Log("Exploding the UFO...");
                DestroyMeteorite();
            }
        }
    }
}
