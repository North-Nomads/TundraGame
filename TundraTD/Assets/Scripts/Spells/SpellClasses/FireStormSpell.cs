using System.Collections;
using System.Collections.Generic;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class FireStormSpell : MagicSpell
    {
        [SerializeField] private float pullForce;
        [SerializeField] private float lifetime;
        private Vector3 tornadoCenter;

        private List<MobBehaviour> _affectedMobs;
        public override BasicElement Element => BasicElement.Fire | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            _affectedMobs = new List<MobBehaviour>();
            transform.position = hitInfo.point;
            tornadoCenter = hitInfo.point;
            GetComponent<CapsuleCollider>();
            StartCoroutine(StayAlive());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                
                mob.IsFocusingTarget = true;
                var destination = tornadoCenter + Vector3.up * 4;
                mob.TargetToFocus = destination;
                
                _affectedMobs.Add(mob);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                mob.IsFocusingTarget = false;
                _affectedMobs.Remove(mob);
            }
        }

        private IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(lifetime);

            // Handle landing
            foreach (var mob in _affectedMobs)
            {
                mob.IsMobMoving = false;
                mob.MobModel.Rigidbody.AddForce(Vector3.down * 1000);
                yield return new WaitForSeconds(.3f);
                mob.IsMobMoving = true;
                mob.IsFocusingTarget = false;
            }
            
            Destroy(gameObject);
        }
    }
}