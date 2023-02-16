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
        public override int MaxTicksAmount { get; }

        public override EffectCode Code => EffectCode.Slowness;

        public float SlownessAmount { get; }

        public SlownessEffect(float amount, int time)
        {
            SlownessAmount = amount;
            MaxTicksAmount = time;
        }

        public override void HandleTick(MobBehaviour mob)
        {
            CurrentTicksAmount++;
        }

        public override void OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed *= SlownessAmount;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed /= SlownessAmount;
        }
    }
}
