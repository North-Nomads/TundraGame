using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class PlaceStunEffect : PlaceBoundEffect
    {
        public override VisualEffectCode Code => VisualEffectCode.None;

        public PlaceStunEffect(Vector3 target, float distance) : base(target, distance)
        {
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed = 0;
            mob.MobModel.Animator.SetBool("IsStunned", true);
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            if (!mob.MobModel.IsAlive)
                return;

            mob.MobModel.CurrentMobSpeed = mob.MobModel.DefaultMobSpeed;
            mob.MobModel.Animator.SetBool("IsStunned", false);
        }
    }
}