using Mobs.MobsBehaviour;
using Spells;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class PebbleStunEffect : Effect
    {
        public int StunTicks { get; }
        public float StunDamage { get; }

        public override int MaxTicksAmount => StunTicks;
        public override EffectCode Code => EffectCode.Stun;
        // I think EffectCode should be renamed to VisualEffectCode

        public override bool OnAttach(MobBehaviour mob)
        {
            Debug.Log("Applied pebbles on mob");
            mob.HitThisMob(StunDamage, BasicElement.Earth);
            return true;
        }

        public PebbleStunEffect(int stunTicks, float stunDamage)
        {
            StunTicks = stunTicks;
            StunDamage = stunDamage;
        }
    }
}