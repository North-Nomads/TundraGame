using System.Linq;
using Mobs.MobsBehaviour;
using Spells;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class SpikesStunEffect : Effect
    {
        private float _stunDamage = 50f;
        public override int MaxTicksAmount { get; }

        public override VisualEffectCode Code => VisualEffectCode.Stun;

        public SpikesStunEffect(int time)
        {
            MaxTicksAmount = time;
        }
        
        public override bool OnAttach(MobBehaviour mob)
        {
            if (mob.CurrentEffects.Any(x => x is SpikesStunEffect))
                return false;
            
            mob.MobModel.MobNavMeshAgent.enabled = false;
            
            mob.HitThisMob(_stunDamage, BasicElement.Earth, "Earth.Stun");
            mob.MobModel.MobNavMeshAgent.angularSpeed = 0;
            mob.MobModel.Animator.SetBool("IsStunned", true);
            return true;
        }
        
        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.enabled = true;
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.DefaultDestinationPoint.position);
            mob.MobModel.MobNavMeshAgent.angularSpeed = mob.MobModel.DefaultMobAngularSpeed;
            mob.MobModel.Animator.SetBool("IsStunned", false);
        }
    }
}