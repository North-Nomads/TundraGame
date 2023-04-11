using System.Linq;
using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    public class StunEffect : Effect
    {
        private const float StunDamage = 50f;
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
            
            mob.MobModel.MobNavMeshAgent.angularSpeed = 0;
            mob.MobModel.MobNavMeshAgent.enabled = false;
            mob.HitThisMob(StunDamage, BasicElement.Earth, "Earth.Stun");
            mob.MobModel.Animator.SetBool("IsStunned", true);
            return true;
        }
        
        public override void OnDetach(MobBehaviour mob)
        {
            if (!mob.MobModel.IsAlive)
                return;
            
            mob.MobModel.MobNavMeshAgent.enabled = true;
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.DefaultDestinationPoint.position);
            mob.MobModel.CurrentMobAngularSpeed = mob.MobModel.DefaultMobAngularSpeed;
            mob.MobModel.Animator.SetBool("IsStunned", false);
        }
    }
}