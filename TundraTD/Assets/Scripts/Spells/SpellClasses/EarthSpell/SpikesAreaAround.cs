using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    public class SpikesAreaAround : MonoBehaviour
    {
        [SerializeField] private ParticleSystem cloudEffect;
        public BoxCollider BoxCollider { get; set; }

        private void Start()
        {
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var mob = other.GetComponent<MobBehaviour>();
            mob.AddReceivedEffects(new List<Effect> { new FearEffect(4) });
        }
    }
}