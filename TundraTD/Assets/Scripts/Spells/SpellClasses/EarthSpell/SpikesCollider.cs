using System.Linq;
using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpikesCollider : MonoBehaviour
    {
        private int _slownessTicks;
        private int _slownessPercent;
        private BoxCollider _boxCollider;
        private Vector3 _halfHeight;

        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _halfHeight = new Vector3(0, _boxCollider.size.y / 2, 0);
        }
        
        private void SetColliderRotation(Vector3 finish) => transform.LookAt(finish + _halfHeight);
        
        private void SetColliderSize(int size) => _boxCollider.size = new Vector3(3, 1, size);

        private void SetColliderCenter(IReadOnlyCollection<Transform> spikes)
        {
            var sum = spikes.Aggregate(Vector3.zero, (current, spike) => current + spike.transform.position);
            transform.position = sum / spikes.Count + _halfHeight;
        }

        private void OnTriggerEnter(Collider other)
        {
            var mob = other.GetComponent<MobBehaviour>();
            Debug.Log($"Before: {mob.MobModel.CurrentMobSpeed}");
            mob.AddReceivedEffects(new List<Effect> { new SlownessEffect(_slownessTicks, _slownessPercent) });
            Debug.Log($"After: {mob.MobModel.CurrentMobSpeed}");
        }

        public void SetColliderParameters(IReadOnlyCollection<Transform> spikes, Vector3 finish)
        {
           SetColliderCenter(spikes);
           SetColliderSize(spikes.Count);
           SetColliderRotation(finish);
        }

        public void SendSlownessValues(int ticks, int percent)
        {
            _slownessTicks = ticks;
            _slownessPercent = percent;
        }
    }
}