﻿using System.Linq;
using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Prevents mob from doing anything by itself: movement or actions
    /// </summary>
    public class StunEffect : Effect
    {
        public override int MaxTicksAmount { get; }

        public override VisualEffectCode Code => VisualEffectCode.Stun;

        public StunEffect(int time)
        {
            MaxTicksAmount = time;
        }
        
        public override bool OnAttach(MobBehaviour mob)
        {
            var stun = mob.CurrentEffects.OfType<StunEffect>().FirstOrDefault();
            if (!(stun is null))
                mob.CurrentEffects.Remove(stun);

            
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