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

        private List<MobBehaviour> _affectedMobs;
        public override BasicElement Element => BasicElement.Fire | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            _affectedMobs = new List<MobBehaviour>();
            transform.position = hitInfo.point;
            GetComponent<CapsuleCollider>();
            StartCoroutine(StayAlive(transform.position));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                mob.IsFollowingPath = false;
                _affectedMobs.Add(mob);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                mob.IsFollowingPath = true;
                _affectedMobs.Remove(mob);
            }
        }

        private IEnumerator StayAlive(Vector3 hit)
        {
            // get all affected by spell and disable movement for these mobs (OnTrigger...)

            // pull mobs towards center in while loop and apply damage
            float time = 0;
            while (time < lifetime)
            {
                foreach (var mob in _affectedMobs)
                {
                    var destination = hit + Vector3.up * 4;
                    //Debug.DrawLine(mob.transform.position, destination);
                    var direction = destination - mob.transform.position;
                    mob.GetComponent<MobBehaviour>().MobModel.Rigidbody.AddForce(direction.normalized * pullForce);
                }
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
            }

            foreach (var mob in _affectedMobs)
            {
                mob.MobModel.Rigidbody.AddForce(Vector3.down * 10);
                yield return new WaitForSeconds(.3f);
                mob.IsFollowingPath = true;
            }
            
            Destroy(gameObject);
        }
    }
}