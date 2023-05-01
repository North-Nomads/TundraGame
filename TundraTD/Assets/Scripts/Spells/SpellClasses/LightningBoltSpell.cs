using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Spells.SpellClasses
{
    
    public class LightningBoltSpell : MagicSpell
    {
        private readonly List<MobBehaviour> _mobsInRadius = new List<MobBehaviour>();
        [SerializeField] private LineRenderer lightning;
        [SerializeField] private float directDamage;
        [SerializeField] private int amountOfBounces;

        public override BasicElement Element => BasicElement.Lightning;

        public override void ExecuteSpell(RaycastHit hit)
        {
            Collider[] collidersInRadius = Physics.OverlapSphere(hit.point, 10, ~0, QueryTriggerInteraction.Ignore);

            foreach (Collider collider in collidersInRadius)
            {
                if(collider != null && collider.gameObject.TryGetComponent<MobBehaviour>(out var mobBehaviour))
                    _mobsInRadius.Add(mobBehaviour);
            }

            if(_mobsInRadius.Count == 0)
            {
                Destroy(gameObject);
                return;
            }

            StartCoroutine(HitMobs(hit.point));
        }

        /// <summary>
        /// Used to redirect strike in another position
        /// </summary>
        /// <param name="hitCoordinates" >
        /// Coordinates of initial impact
        /// </param>
        /// <param name="redirectCoordinates">
        /// Strike destination cooridnates
        /// </param>
        public void OverrideStrike(Vector3 hitCoordinates, Vector3 redirectCoordinates)
        {
            StopAllCoroutines();
            StartCoroutine(RenderOverride(hitCoordinates, redirectCoordinates));
        }

        private MobBehaviour GetClosestMob(Vector3 currentMobPosition)
        {
            if (!_mobsInRadius.Any())
                return null;
            MobBehaviour closestMob = _mobsInRadius[0];
            foreach(MobBehaviour mob in _mobsInRadius)
            {
                if((currentMobPosition - mob.transform.position).magnitude < (currentMobPosition - closestMob.transform.position).magnitude)
                    closestMob = mob;
            }
            Debug.DrawLine(currentMobPosition, closestMob.transform.position);
            return closestMob;
        }

        private IEnumerator HitMobs(Vector3 hitPosition)
        {
            MobBehaviour mobToStrike = GetClosestMob(hitPosition);
            lightning.SetPosition(0, hitPosition);
            lightning.SetPosition(1, mobToStrike.transform.position);
            for(int bounce = amountOfBounces; bounce > 0; --bounce) 
            {
                yield return new WaitForSeconds(.1f);
                if(GetClosestMob(mobToStrike.transform.position) != null)
                {
                    mobToStrike = GetClosestMob(mobToStrike.transform.position);
                    _mobsInRadius.Remove(mobToStrike);
                    lightning.SetPosition(0, mobToStrike.transform.position);
                    if (GetClosestMob(mobToStrike.transform.position) != null)
                        lightning.SetPosition(1, GetClosestMob(hitPosition).transform.position);
                    else
                        lightning.enabled = false;

                }
                mobToStrike.HitThisMob(directDamage, BasicElement.Lightning);
            }
            Destroy(gameObject);
            yield return null;
        }

        private IEnumerator RenderOverride(Vector3 startCords, Vector3 endCords)
        {
            lightning.SetPosition(0, startCords);
            lightning.SetPosition(1, endCords);
            yield return new WaitForSeconds(.1f);
            Destroy(gameObject);
            yield return null;
        }
    }
}