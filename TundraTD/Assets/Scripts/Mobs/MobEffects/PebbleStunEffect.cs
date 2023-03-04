using Mobs.MobsBehaviour;
using Spells;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class PebbleStunEffect : Effect
    {
        private int StunTicks { get; }
        private float StunDamage { get; }

        public override int MaxTicksAmount => StunTicks;
        public override VisualEffectCode Code => VisualEffectCode.Stun;
        // I think EffectCode should be renamed to VisualEffectCode

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.HitThisMob(StunDamage, BasicElement.Earth, "EarthMod.Pebbles");
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.transform.position);
            mob.MobModel.MobNavMeshAgent.angularSpeed = 0;
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.DefaultDestinationPoint.position);
            mob.MobModel.MobNavMeshAgent.angularSpeed = mob.MobModel.DefaultMobAngularSpeed;
        }

        public PebbleStunEffect(int stunTicks, float stunDamage)
        {
            StunTicks = stunTicks;
            StunDamage = stunDamage;
        }
    }
}