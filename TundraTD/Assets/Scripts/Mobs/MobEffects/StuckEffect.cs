using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    public class StuckEffect : Effect
    {
        public override int MaxTicksAmount { get; }
        public override VisualEffectCode Code => VisualEffectCode.None;
        
        public override bool OnAttach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.angularSpeed = 0;
            mob.MobModel.MobNavMeshAgent.enabled = false;
            mob.MobModel.Animator.SetBool("IsStunned", true);
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.enabled = true;
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.DefaultDestinationPoint.position);
            mob.MobModel.CurrentMobAngularSpeed = mob.MobModel.DefaultMobAngularSpeed;
            mob.MobModel.Animator.SetBool("IsStunned", true);
        }
        
        public StuckEffect(int time)
        {
            MaxTicksAmount = time;
        }
    }
}