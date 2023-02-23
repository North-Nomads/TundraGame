using System;
using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;
using UnityEngine.UIElements;

namespace Spells.SpellClasses.EarthSpell
{
    public class SpikesAreaAround : MonoBehaviour
    {
        public BoxCollider BoxCollider { get; set; }

        private void Start()
        {
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var mob = other.GetComponent<MobBehaviour>();
            mob.AddReceivedEffects(new List<Effect> { new DisorientationEffect(4) });
        }
    }
}