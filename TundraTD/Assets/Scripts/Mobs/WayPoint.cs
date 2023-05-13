using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs
{
    /// <summary>
    /// A point mob goes towards to 
    /// </summary>
    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private bool isSoftGround;

        public bool IsSoftGround => isSoftGround;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                mob.HandleWaypointApproachingOrPassing();
            }
        }
    }
}