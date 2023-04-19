using Mobs.MobsBehaviour;
using Mobs.MobsBehaviour.Eagle;
using System.Linq;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class WetEffect : Effect
    {
        public override int MaxTicksAmount { get; }

        public override VisualEffectCode Code => VisualEffectCode.Wet;

        public WetEffect(int time)
        {
            MaxTicksAmount = time;
        }
    }
}