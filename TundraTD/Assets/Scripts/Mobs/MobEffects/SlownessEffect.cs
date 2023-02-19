using Mobs.MobsBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobs.MobEffects
{
    internal class SlownessEffect : Effect
    {
        public float SpeedModifier { get; }
        public override int MaxTicksAmount { get; }
        public override EffectCode Code => EffectCode.Slowness;

        public SlownessEffect(float modifier, int time)
        {
            SpeedModifier = modifier;
            MaxTicksAmount = time;
        }

        public override void HandleTick(MobBehaviour mob)
        {
            CurrentTicksAmount++;
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
