using Mobs.MobsBehaviour;
using System.Linq;
using UnityEngine;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Forces mob to move towards different point
    /// </summary>
    public class DistractEffect : Effect
    {
        private readonly WayPoint _target;

        public override int MaxTicksAmount { get; }

        public override VisualEffectCode Code => VisualEffectCode.Distract;

        public DistractEffect(WayPoint target, int time)
        {
            _target = target;
            MaxTicksAmount = time;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            // Inserting waypoint to current index, mob has to automatically focus on a new target
            // Once the waypoint is reached, mob current waypoint index will increase and the target will return back 
            mob.WaypointRoute.Insert(mob.CurrentWaypointIndex, _target);
            return !mob.CurrentEffects.OfType<DistractEffect>().Any();
        }

        public override void OnDetach(MobBehaviour mob)
        {
            // If mob hasn't yet reached the point (so his currentWp == target) - remove this target  
            if (Vector3.Distance(mob.WaypointRoute[mob.CurrentWaypointIndex].transform.position, _target.transform.position) < .5f)
                mob.WaypointRoute.RemoveAt(mob.CurrentWaypointIndex);
            
            // Anyways, the target will return to the default one because reaching the
            // waypoint automatically increase the index for the mob 
        }
    }
}