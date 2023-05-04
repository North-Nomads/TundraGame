using System.Linq;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class BallLightningSpell : MagicSpell
    {
        private float _time;

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
            _time += Time.deltaTime;
            if (_time >= timeToDetonate)
                Detonate();
        }
        private void Detonate()
        {
            Physics.OverlapSphere(transform.position, detonationRadius).ToList().ForEach(coll =>
            {
                if (coll.gameObject.TryGetComponent(out MobBehaviour mob))
                    mob.HitThisMob(Mathf.Lerp(minDamage, maxDamage, _time / timeToDetonate), BasicElement.Lightning);
            });
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Detonate();
        }
    }
}