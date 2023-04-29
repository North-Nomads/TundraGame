using System.Collections.Generic;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class LightningCrystalSpell : MagicSpell
    {
        private readonly List<Rigidbody> _mobs = new List<Rigidbody>();
        private float _time;

        [SerializeField] private float pullForce;
        [SerializeField] private float timeToLive;
        public override BasicElement Element => BasicElement.Air | BasicElement.Lightning;

        private void OnTriggerEnter(Collider e)
        {
            if (!e.gameObject.TryGetComponent(out Rigidbody enteredMob))
                return;
            _mobs.Add(enteredMob);
        }

        private void OnTriggerExit(Collider e)
        {
            if (!e.gameObject.TryGetComponent(out Rigidbody enteredMob))
                return;
            _mobs.Remove(enteredMob);
        }

        private void FixedUpdate()
        {
            _mobs.ForEach(mob => { if (!mob.gameObject.activeSelf) _mobs.Remove(mob); });

            _mobs.ForEach(mob => mob.AddForce((mob.transform.position - transform.position).normalized * (pullForce * -1),
                ForceMode.Force));
        }

        private void Start()
        {
            var colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
            foreach (var overlappedCollider in colliders)
                if (overlappedCollider.gameObject.TryGetComponent(out Rigidbody mob))
                    _mobs.Add(mob);
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time > timeToLive)
                Destroy(gameObject);
        }
        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            gameObject.transform.position = hitInfo.point;
        }
    }
}
