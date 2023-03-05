using Mobs.MobsBehaviour;
using System.Linq;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class FreezeEffect : Effect
    {
        public float SpeedModifier { get; set; }
        
        public float MinSpeedModifier { get; }
        
        public float TimeModifier { get; }

        public bool ContinueFreeze { get; set; }
        
        public override int MaxTicksAmount { get; }
        
        public override VisualEffectCode Code => VisualEffectCode.Freeze;

        public FreezeEffect(float minModifier, float timeModifier, int time)
        {
            MinSpeedModifier = minModifier;
            TimeModifier = timeModifier;
            MaxTicksAmount = time;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            ContinueFreeze = true;
            SpeedModifier = mob.MobModel.DefaultMobAngularSpeed;
            return !mob.CurrentEffects.OfType<FreezeEffect>().Any();
        }

        public override void HandleTick(MobBehaviour mob)
        {
            base.HandleTick(mob);
            float step = (mob.MobModel.DefaultMobAngularSpeed - MinSpeedModifier) * BasicTickTime / TimeModifier;
            if (ContinueFreeze)
            {
                step *= -1;
                ContinueFreeze = false;
            }
            SpeedModifier += step;
            SpeedModifier = Mathf.Clamp(SpeedModifier, MinSpeedModifier, mob.MobModel.DefaultMobAngularSpeed);
            mob.MobModel.CurrentMobSpeed = SpeedModifier;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed = mob.MobModel.DefaultMobAngularSpeed;
        }
    }
}