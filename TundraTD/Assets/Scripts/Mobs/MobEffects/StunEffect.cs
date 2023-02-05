using Mobs.MobsBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobs.MobEffects
{
    public class StunEffect : Effect
    {
        public override int MaxTicksAmount => 5;

        public override EffectCode Code => EffectCode.Stun;

        public override void HandleTick(MobBehaviour mob)
        {
        }

        public override void OnAttach(MobBehaviour mob)
        {
            // TODO: Well then, I need some modifications in the mob system to implement this feature.
        }
    }
}
