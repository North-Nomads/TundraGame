﻿using System.Collections.Generic;
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
            _meteors[0].Explode();
            _cooldownTime = maxCooldownTime;
            _meteors.Clear();
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
