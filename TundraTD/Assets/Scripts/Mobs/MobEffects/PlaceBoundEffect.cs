using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
	public abstract class PlaceBoundSpell : Effect
	{
		public Vector3 TargetPosition { get; set; }

		public float Distance { get; set; }

        public MobBehaviour TargetMob { get; }

        public override bool ShouldBeDetached => (TargetMob.transform.position - TargetPosition).sqrMagnitude > Distance * Distance;

        protected PlaceBoundSpell(Vector3 target, float distance)
        {
            TargetPosition = target;
            Distance = distance;
        }
    }
}