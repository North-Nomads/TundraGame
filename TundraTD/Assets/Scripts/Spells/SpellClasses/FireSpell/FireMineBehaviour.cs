using Mobs.MobsBehaviour;
using System.Collections;
using UnityEngine;

namespace Spells
{
	public class FireMineBehaviour : MonoBehaviour
	{
        const float Lifetime = 10;
        const float ExplosionLifetime = 1;
        const float Radius = 10;
        const int mobsMask = 1 << 8;
        const float Damage = 15;
        [SerializeField] private GameObject explosionPrefab;
        private Collider[] _mobColliders = new Collider[10];

        private void Start()
        {
            StartCoroutine(StayAlive());
        }

        private IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(Lifetime);
            Destroy(gameObject);
        }

        private IEnumerator CastExplosion()
        {
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            int mobs = Physics.OverlapSphereNonAlloc(transform.position, Radius, _mobColliders, mobsMask);
            for (int i = 0; i < mobs; i++)
            {
                var mob = _mobColliders[i].GetComponent<MobBehaviour>();
                mob.HitThisMob(Damage * Vector3.Distance(transform.position, _mobColliders[i].transform.position) / Radius, BasicElement.Fire);
                mob.GetComponent<Rigidbody>().AddExplosionForce(Radius, transform.position, Radius);
            }
            Destroy(gameObject);
            yield return new WaitForSeconds(ExplosionLifetime);
            Destroy(explosion);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            StartCoroutine(CastExplosion());
        }
    }
}