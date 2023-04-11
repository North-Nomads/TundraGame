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
        public Transform Target;

        public override int MaxTicksAmount { get; }

        public override VisualEffectCode Code => VisualEffectCode.Distract;

        public DistractEffect(Transform target, int time)
        {
            Target = target;
            MaxTicksAmount = time;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            if (Target != null) mob.CurrentDestinationPoint = Target.position;
            return !mob.CurrentEffects.OfType<DistractEffect>().Any();
        }

        public override void HandleTick(MobBehaviour mob)
        {
            base.HandleTick(mob);
            if (Target != null) mob.CurrentDestinationPoint = Target.position;
            else mob.CurrentDestinationPoint = mob.DefaultDestinationPoint.position;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.CurrentDestinationPoint = mob.DefaultDestinationPoint.position;
        }
    }
}