using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using Spells;
using System.Collections;
using UnityEngine;

namespace Spells
{
    public class GhostBehaviour : MonoBehaviour
    {
        const float Lifetime = 2;
        const float ExplosionLifetime = 1;
        const int DistractTime = 3;
        const float Damage = 15;
        const float Radius = 10;
        const int MobsMask = 1 << 8;
        [SerializeField] private GameObject explosionPrefab;
        private readonly Collider[] _mobColliders = new Collider[50];        
        private float _currentTime;


        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > Lifetime)
            {
                Destroy(gameObject);
                return;
            }

            int mobs = Physics.OverlapSphereNonAlloc(transform.position, Radius, _mobColliders, MobsMask);
            for (int i = 0; i < mobs; i++)
            {
                var mob = _mobColliders[i].GetComponent<MobBehaviour>();
                if (mob != null)
                    mob.AddSingleEffect(new DistractEffect(transform, DistractTime));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            int mobs = Physics.OverlapSphereNonAlloc(transform.position, Radius, _mobColliders, MobsMask);
            for (int i = 0; i < mobs; i++)
            {
                var mob = _mobColliders[i].GetComponent<MobBehaviour>();
                mob.RemoveFilteredEffects(x => x is DistractEffect);
                mob.HitThisMob(Damage * Vector3.Distance(transform.position, _mobColliders[i].transform.position) / Radius, BasicElement.Fire, nameof(GhostBehaviour));
                mob.GetComponent<Rigidbody>().AddExplosionForce(Radius, transform.position, Radius);
            }
            Destroy(gameObject);
        }
    }
}