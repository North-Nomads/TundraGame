using Mobs.MobsBehaviour;
using System;
using System.Linq;
using UnityEngine;

namespace Spells
{
    public class BallLightningSpell : MagicSpell
    {
        private float t = 0;

        [SerializeField] private float timeToDetonate;
        [SerializeField] private float detonationRadius;
        [SerializeField] private float maxDamage;
        [SerializeField] private float minDamage;
        public override BasicElement Element => BasicElement.Fire | BasicElement.Lightning;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            gameObject.transform.position = hitInfo.point;
        }


        private void Update()
        {
            t += Time.deltaTime;
            if (t >= timeToDetonate)
                Detonate();
        }
        private void Detonate()
        {
            Physics.OverlapSphere(transform.position, detonationRadius).ToList().ForEach(coll =>
            {
                if (coll.gameObject.TryGetComponent(out MobBehaviour mob))
                    mob.HitThisMob(Mathf.Lerp(minDamage, maxDamage, t / timeToDetonate), BasicElement.Lightning, "Ball lightning");
            });
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Detonate();
        }
    }
}