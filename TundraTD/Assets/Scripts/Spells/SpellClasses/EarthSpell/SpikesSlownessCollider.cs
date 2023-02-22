using System.Linq;
using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpikesSlownessCollider : MonoBehaviour
    {
        private int _slownessTicks;
        private float _slownessPercent;
        private Vector3 _halfHeight;

        public float SpikesEnterDamage { get; set; }
        public BoxCollider BoxCollider { get; private set; }

        private void Start()
        {
            BoxCollider = GetComponent<BoxCollider>();
            _halfHeight = new Vector3(0, BoxCollider.size.y / 2, 0);
        }
        
        private void SetColliderRotation(Vector3 finish) => transform.LookAt(finish + _halfHeight);
        
        private void SetColliderSize(int size) => BoxCollider.size = new Vector3(3, 1, size);

        private void SetColliderCenter(List<SpikesGroup> spikes)
        {
            var sum = spikes.Aggregate(Vector3.zero, (current, spike) => current + spike.transform.position);
            transform.position = sum / spikes.Count + _halfHeight;
        }

        private void OnTriggerEnter(Collider other)
        {
            var mob = other.GetComponent<MobBehaviour>();
            mob.AddReceivedEffects(new List<Effect> { new SlownessEffect(_slownessPercent, _slownessTicks) });
            mob.HitThisMob(SpikesEnterDamage, BasicElement.Earth);
        }

        public void SetColliderParameters(List<SpikesGroup> spikes, Vector3 finish)
        {
           SetColliderCenter(spikes);
           SetColliderSize(spikes.Count);
           SetColliderRotation(finish);
        }

        public void SendSlownessValues(int ticks, float modifier)
        {
            _slownessTicks = ticks;
            _slownessPercent = modifier;
        }
    }
}