using Mobs.MobsBehaviour;
using System.Linq;
using UnityEngine;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Softly slows mob up to full stop  
    /// </summary>
    public class FreezeEffect : TimeBoundEffect
    {
        public float SpeedModifier { get; set; }
        
        public float MinSpeedModifier { get; }
        
        public float TimeModifier { get; }

        public bool ContinueFreeze { get; set; }
        
        public override VisualEffectCode Code => VisualEffectCode.Freeze;

        public FreezeEffect(float minModifier, float timeModifier, int time) : base(time)
        {
            MinSpeedModifier = minModifier;
            TimeModifier = timeModifier;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            ContinueFreeze = true;
            SpeedModifier = mob.MobModel.DefaultMobSpeed;
            return !mob.CurrentEffects.OfType<FreezeEffect>().Any();
        }

        public override void HandleTick(MobBehaviour mob)
        {
            base.HandleTick(mob);
            float step = (mob.MobModel.DefaultMobSpeed - MinSpeedModifier) * BasicTickTime / TimeModifier;
            if (ContinueFreeze)
            {
                step *= -1;
                ContinueFreeze = false;
            }
            SpeedModifier += step;
            SpeedModifier = Mathf.Clamp(SpeedModifier, MinSpeedModifier, mob.MobModel.DefaultMobSpeed);
            mob.MobModel.CurrentMobSpeed = SpeedModifier;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed = mob.MobModel.DefaultMobSpeed;
        }
    }
}