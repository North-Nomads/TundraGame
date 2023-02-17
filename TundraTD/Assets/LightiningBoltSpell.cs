using UnityEngine;
namespace Spells.SpellClasses
{
    public class LightiningBoltSpell : MagicSpell
    {
        public override void ExecuteSpell()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 200, ~0, QueryTriggerInteraction.Ignore))
                return;
            LaunchStrike(hit.transform.position, 5);
        }

        private void LaunchStrike(Vector3 origin, int strikesLeft)
        {
            Vector3 ClosestEnemy = new Vector3();
            if(strikesLeft == 0)
                return;
            RaycastHit hit;
            
            if(Physics.Linecast(origin, ClosestEnemy, out hit, ~0, QueryTriggerInteraction.Ignore))
                return;


            LaunchStrike(hit.transform.position, strikesLeft - 1);
        }
    }
}