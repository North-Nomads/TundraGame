using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class LavaBurningEffect : PlaceBoundEffect
    {
        public override VisualEffectCode Code => VisualEffectCode.None;

        public float Damage { get; set; }

        public LavaBurningEffect(Vector3 target, float damage, float distance) : base(target, distance)
        {
            Damage = damage;
        }

        public override void HandleTick(MobBehaviour mob)
        {
            base.HandleTick(mob);
            mob.HitThisMob(Damage, Spells.BasicElement.Fire);
        }
    }
}