using Mobs.MobsBehaviour;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Spells.SpellClasses
{
    [Spell(BasicElement.Lightning, "Lightning", "Shocking!")]
    public class LightiningBoltSpell : MagicSpell
    {
        private List<MobBehaviour> MobsInRadius;

        public override void InstantiateSpellExecution()
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.Log("Lightning Stroke");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 200, ~0, QueryTriggerInteraction.Ignore))
                return;
            Collider[] collidersInRadius =  new Collider[200];
            Physics.OverlapSphereNonAlloc(hit.transform.position, 10, collidersInRadius, ~0, QueryTriggerInteraction.Ignore);
            foreach (Collider collider in collidersInRadius)
            {
                if(collider.gameObject.TryGetComponent<MobBehaviour>(out var mobBehaviour))
                    MobsInRadius.Add(mobBehaviour);
            }
            
            LaunchStrike(GetClosestMob(hit.transform.position),5);
        }

        private void LaunchStrike(MobBehaviour mobToStrike, int strikesLeft)
        {
            mobToStrike.HitThisMob(100, BasicElement.Lightning);
            MobsInRadius.Remove(mobToStrike);
            if(strikesLeft == 0)
                return;
            


            LaunchStrike(GetClosestMob(mobToStrike.transform.position), strikesLeft - 1);
        }

        private MobBehaviour GetClosestMob(Vector3 CurrentMobPosition)
        {
            MobBehaviour closestMob = MobsInRadius[0];
            foreach(MobBehaviour mob in MobsInRadius)
            {
                if((CurrentMobPosition - mob.transform.position).magnitude < (CurrentMobPosition - closestMob.transform.position).magnitude)
                    closestMob = mob;
            }
            return closestMob;
        }
    }
}