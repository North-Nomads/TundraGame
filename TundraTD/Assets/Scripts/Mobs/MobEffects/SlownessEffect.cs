using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class SlownessEffect : Effect
    {
        private readonly int _slownessPercent;
        public override int MaxTicksAmount { get; }
        public override EffectCode Code => EffectCode.Slowness;

        public SlownessEffect(int ticks, int percent)
        {
            MaxTicksAmount = ticks;
            _slownessPercent = percent;
        }

        public override void OnAttach(MobBehaviour mob)
        {
            Debug.Log("Attach");
            mob.MobModel.CurrentMobSpeed = mob.MobModel.CurrentMobSpeed * _slownessPercent / 100f;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            Debug.Log("Detach");
            mob.MobModel.CurrentMobSpeed = mob.MobModel.CurrentMobSpeed * 100f / _slownessPercent;
        }
    }
}