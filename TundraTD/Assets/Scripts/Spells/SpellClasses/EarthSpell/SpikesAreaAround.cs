using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    public class SpikesAreaAround : MonoBehaviour
    {
        public BoxCollider BoxCollider { get; private set; }
        private const float FearDuration = 1f;  

        private void Start()
        {
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var mob = other.GetComponent<MobBehaviour>();
            mob.AddSingleEffect(new FearEffect(FearDuration.SecondsToTicks()));
        }
    }
}