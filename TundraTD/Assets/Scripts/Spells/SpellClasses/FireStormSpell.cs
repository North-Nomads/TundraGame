using System.Collections;
using System.Collections.Generic;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class FireStormSpell : MagicSpell
    {
        [SerializeField] private float pullForce;
        [SerializeField] private float lifetime;
        private Vector3 _tornadoCenter;

        private List<MobBehaviour> _affectedMobs;
        public override BasicElement Element => BasicElement.Fire | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            _affectedMobs = new List<MobBehaviour>();
            transform.position = hitInfo.point;
            _tornadoCenter = hitInfo.point;
            GetComponent<CapsuleCollider>();
            StartCoroutine(StayAlive());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                
                var destination = _tornadoCenter + Vector3.up * 4;
                mob.SetFocusingTarget(destination);
                
                mob.AddSingleEffect(new BurningEffect(.5f, 6f.SecondsToTicks(), false));
                _affectedMobs.Add(mob);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                mob.SetFocusingTarget(null);
                _affectedMobs.Remove(mob);
            }
        }

        private IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(lifetime);

            // Handle landing
            foreach (var mob in _affectedMobs)
            {
                mob.IsMoving = false;
                mob.MobModel.Rigidbody.AddForce(Vector3.down * 1000);
                yield return new WaitForSeconds(.3f);
                mob.IsMoving = true;
                mob.SetFocusingTarget(null);
            }
            
            Destroy(gameObject);
        }
    }
}