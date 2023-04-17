using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Spells
{
	public class SwampSpell : MagicSpell
	{
        public override BasicElement Element => BasicElement.Earth | BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            StartCoroutine(StayAlive());
        }

        IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }


    }
}