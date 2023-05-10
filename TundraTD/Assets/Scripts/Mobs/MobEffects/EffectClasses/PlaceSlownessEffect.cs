using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class PlaceSlownessEffect : PlaceBoundEffect
    {
        public override VisualEffectCode Code => VisualEffectCode.None;

        public float SpeedModifier { get; set; }

        public PlaceSlownessEffect(Vector3 target, float amount, float distance) : base(target, distance)
        {
            SpeedModifier = amount;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed *= SpeedModifier;
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed /= SpeedModifier;
        }
    }
}