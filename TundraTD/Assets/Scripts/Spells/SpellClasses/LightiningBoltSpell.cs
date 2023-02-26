using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spells.SpellClasses
{
    [Spell(BasicElement.Lightning, "Lightning", "Shocking!")]
    public class LightiningBoltSpell : MagicSpell
    {
        private List<MobBehaviour> _mobsInRadius = new List<MobBehaviour>();
        [SerializeField] private LineRenderer lightining;
        public override void InstantiateSpellExecution()
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.Log("Lightning Stroke");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, float.PositiveInfinity, 1<<8|1<<10))
                return;
            Collider[] collidersInRadius =  new Collider[200];
            Physics.OverlapSphereNonAlloc(hit.point, 10, collidersInRadius, ~0, QueryTriggerInteraction.Ignore);
            foreach (Collider collider in collidersInRadius)
            {
                if(collider != null && collider.gameObject.TryGetComponent<MobBehaviour>(out var mobBehaviour))
                    _mobsInRadius.Add(mobBehaviour);
            }
            lightining.enabled = true;
            lightining.SetPosition(0, hit.point);
            if(_mobsInRadius.Count == 0)
            {
                Debug.Log("Miss");
                return;
            }
            
            lightining.SetPosition(1, GetClosestMob(hit.point).transform.position);
            LaunchStrike(GetClosestMob(hit.point),5);
        }

        private void LaunchStrike(MobBehaviour mobToStrike, int strikesLeft)
        {
            mobToStrike.HitThisMob(10000, BasicElement.Lightning);
            _mobsInRadius.Remove(mobToStrike);
            if(strikesLeft == 0 || _mobsInRadius.Count == 0)
                return;
           


            LaunchStrike(GetClosestMob(mobToStrike.transform.position), strikesLeft - 1);
        }

        private MobBehaviour GetClosestMob(Vector3 CurrentMobPosition)
        {
            MobBehaviour closestMob = _mobsInRadius[0];
            foreach(MobBehaviour mob in _mobsInRadius)
            {
                if((CurrentMobPosition - mob.transform.position).magnitude < (CurrentMobPosition - closestMob.transform.position).magnitude)
                    closestMob = mob;
            }
            Debug.DrawLine(CurrentMobPosition, closestMob.transform.position);
            return closestMob;
        }

        private IEnumerator DrawLightining()
        {

            yield return null;
        }
        private void OnDrawGizmos()
        {
            
        }
    }
}