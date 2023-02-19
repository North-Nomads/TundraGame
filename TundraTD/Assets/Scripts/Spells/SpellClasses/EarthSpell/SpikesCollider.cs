using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpikesCollider : MonoBehaviour
    {
        private BoxCollider _boxCollider;
        private Vector3 _halfHeight;

        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _halfHeight = new Vector3(0, _boxCollider.size.y / 2, 0);
        }

        public void SetColliderParameters(IReadOnlyCollection<SpikesSpell> spikes, Vector3 finish)
        {
           SetColliderCenter(spikes);
           SetColliderSize(spikes.Count);
           SetColliderRotation(finish);
        }

        private void SetColliderRotation(Vector3 finish)
        {
            transform.LookAt(finish + _halfHeight);
        }

        private void SetColliderSize(int size)
        {
            _boxCollider.size = new Vector3(3, 1, size);
        }

        private void SetColliderCenter(IReadOnlyCollection<SpikesSpell> spikes)
        {
            var sum = spikes.Aggregate(Vector3.zero, (current, spike) => current + spike.transform.position);
            transform.position = sum / spikes.Count + _halfHeight;
        }
    }
}