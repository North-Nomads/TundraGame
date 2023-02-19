using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class SlownessEffect : Effect
    {
        public float SpeedModifier  { get; }
        public override int MaxTicksAmount { get; }
        public override EffectCode Code => EffectCode.Slowness;

        public SlownessEffect(float modifier, int time)
        {
            SpeedModifier = modifier;
            MaxTicksAmount = time;
        }

        public override void OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed *= SpeedModifier;   
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed /= SpeedModifier;
        }
    }
}