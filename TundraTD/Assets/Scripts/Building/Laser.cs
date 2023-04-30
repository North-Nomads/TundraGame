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
        [SerializeField] private float interactionSize;

        private List<MeteorSpell> _meteors;
        private float _cooldownTime;

        protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
        {
            var spell = (MagicSpell)sender;
            
            // TODO: Extract this function in Lightning
            /*if (spell.Element == BasicElement.Lightning && (e.HitInfo.point - transform.position).sqrMagnitude < interactionSize * interactionSize)             
                _cooldownTime = maxCooldownTime;*/

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
            for (int i = 0; i < _meteors.Count; )
            {
                var meteor = _meteors[i];
                Debug.Log($"{i}/{_meteors.Count}");

                if (!(meteor is null))
                {
                    meteor.Explode();
                    _meteors.RemoveAt(i);
                    _cooldownTime = maxCooldownTime;
                    return;
                }
                _meteors.RemoveAt(i);
                return;
            }
        }

        private void FixedUpdate()
        {
            if (_cooldownTime > 0)
            {
                _cooldownTime -= Time.deltaTime;
                return;
            }
            
            Debug.Log("Exploding the UFO...");
            DestroyMeteorite();
        }
        
        
    }
}
