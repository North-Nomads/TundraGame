using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
	public abstract class PlaceBoundEffect : Effect
	{
		public Vector3 TargetPosition { get; set; }

		public float Distance { get; set; }

        public MobBehaviour TargetMob { get; }

        public override bool ShouldBeDetached => (TargetMob.transform.position - TargetPosition).sqrMagnitude > Distance * Distance;

        protected PlaceBoundEffect(Vector3 target, float distance)
        {
            TargetPosition = target;
            Distance = distance;
        }
    }
}