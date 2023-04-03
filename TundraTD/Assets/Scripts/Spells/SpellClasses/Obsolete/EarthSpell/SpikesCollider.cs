using System.Collections;
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
        [SerializeField] private SpikesSpell spell;
        private int _slownessTicks;
        private float _slownessPercent;
        private Vector3 _halfHeight;
        private List<MobBehaviour> _mobsInCollider;
        private SpikesAreaAround _spikesAreaAround;
        private readonly Collider[] _mobsInSolidWall = new Collider[20];

        public float SpikesEnterDamage { get; set; }
        public BoxCollider BoxCollider { get; private set; }
        public float TermitesDamage { get; set; }

        private void Start()
        {
            _mobsInCollider = new List<MobBehaviour>();
            _spikesAreaAround = transform.parent.GetComponentInChildren<SpikesAreaAround>();
            BoxCollider = GetComponent<BoxCollider>();
            _halfHeight = new Vector3(0, BoxCollider.size.y / 2, 0);
        }

        private void SetColliderRotation(Vector3 finish)
        {
            transform.LookAt(finish + _halfHeight);
            _spikesAreaAround.transform.LookAt(finish + _halfHeight);
        } 

        private void SetColliderSize(int size)
        {
            BoxCollider.size = new Vector3(3, 1, size);
            _spikesAreaAround.BoxCollider.size = BoxCollider.size + new Vector3(4, 0, 2);
        } 

        private void SetColliderCenter(List<SpikesGroup> spikes)
        {
            var sum = spikes.Aggregate(Vector3.zero, (current, spike) => current + spike.transform.position);
            transform.position = sum / spikes.Count + _halfHeight;
            _spikesAreaAround.transform.position = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            var mob = other.GetComponent<MobBehaviour>();
            if (mob is null)
                return;
            
            _mobsInCollider.Add(mob);
            mob.AddReceivedEffects(new List<Effect> { new SlownessEffect(_slownessPercent, _slownessTicks) });
        }

        private void OnTriggerExit(Collider other)
        {
            var mob = other.GetComponent<MobBehaviour>();
            _mobsInCollider.Remove(mob);
        }
        
        private IEnumerator ApplyTermitesHitOnMobs()
        {
            while (true)
            {
                foreach (var mobBehaviour in _mobsInCollider)
                    mobBehaviour.HitThisMob(TermitesDamage, BasicElement.Earth, "EarthMods.Termites");
                
                yield return new WaitForSeconds(1f);
            }
        }

        public void InitializeTermites(bool hasTermites)
        {
            if (!hasTermites)
                return;

            StartCoroutine(ApplyTermitesHitOnMobs());
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

        public void ForcePushOnSolidWalls()
        {
            var size = Physics.OverlapBoxNonAlloc(BoxCollider.transform.position, BoxCollider.size / 2,
                _mobsInSolidWall, BoxCollider.transform.rotation, 1 << 8);
            // TODO: remove comment and fix error.
            //for (int i = 0; i < size; i++)

            //    _mobsInSolidWall[i].GetComponent<MobBehaviour>().AddSingleEffect(new StuckEffect(spell.Lifetime));
        }
    }
}