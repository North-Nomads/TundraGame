using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    public class PebbleStunEffect : Effect
    {
        private int StunTicks { get; }
        private float StunDamage { get; }

        public override int MaxTicksAmount => StunTicks;
        public override VisualEffectCode Code => VisualEffectCode.Stun;

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.HitThisMob(StunDamage, BasicElement.Earth, "EarthMod.Pebbles");
            if (mob.MobModel.MobNavMeshAgent.isActiveAndEnabled)
                mob.MobModel.MobNavMeshAgent.SetDestination(mob.transform.position);
            mob.MobModel.MobNavMeshAgent.angularSpeed = 0;
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            if (!mob.MobModel.MobNavMeshAgent.isActiveAndEnabled)
                return;       
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.DefaultDestinationPoint.position);
            mob.MobModel.CurrentMobSpeed = mob.MobModel.DefaultMobSpeed;
        }

        public PebbleStunEffect(int stunTicks, float stunDamage)
        {
            StunTicks = stunTicks;
            StunDamage = stunDamage;
        }
    }
}