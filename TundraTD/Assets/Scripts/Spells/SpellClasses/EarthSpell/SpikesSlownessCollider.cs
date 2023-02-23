using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;
using UnityEngine.Rendering;

namespace Spells.SpellClasses.EarthSpell
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpikesSlownessCollider : MonoBehaviour
    {
        private int _slownessTicks;
        private float _slownessPercent;
        private Vector3 _halfHeight;
        private List<MobBehaviour> _mobsInCollider;

        public float SpikesEnterDamage { get; set; }
        public BoxCollider BoxCollider { get; private set; }
        public float TermitesDamage { get; set; }

        private void Start()
        {
            _mobsInCollider = new List<MobBehaviour>();
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
            Debug.Log("Add mob");
            var mob = other.GetComponent<MobBehaviour>();
            _mobsInCollider.Add(mob);
            mob.AddReceivedEffects(new List<Effect> { new SlownessEffect(_slownessPercent, _slownessTicks) });
            mob.HitThisMob(SpikesEnterDamage, BasicElement.Earth);
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Remove mob");
            var mob = other.GetComponent<MobBehaviour>();
            _mobsInCollider.Remove(mob);
        }
        
        private IEnumerator ApplyTermitesHitOnMobs()
        {
            while (true)
            {
                Debug.Log("Termites bite");
                foreach (var mobBehaviour in _mobsInCollider)
                {
                    mobBehaviour.HitThisMob(TermitesDamage, BasicElement.Earth);
                }

                yield return new WaitForSeconds(1f);
            }

        }

        public void InitilizeTermites(bool hasTermites)
        {
            if (!hasTermites)
                return;

            StartCoroutine(ApplyTermitesHitOnMobs());
        }

        public void SetColliderParameters(List<SpikesGroup> spikes, Vector3 finish, bool hasTermites)
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