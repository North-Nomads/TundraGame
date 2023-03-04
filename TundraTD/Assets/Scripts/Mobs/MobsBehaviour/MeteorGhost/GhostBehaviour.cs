using Spells;
using System.Collections;
using UnityEngine;

namespace Mobs.MobsBehaviour
{
    public class GhostBehaviour : MonoBehaviour
    {
        private void Start()
        {
            // TODO: implement mob targeting
            StartCoroutine(StayAlive());
        }

        private IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}