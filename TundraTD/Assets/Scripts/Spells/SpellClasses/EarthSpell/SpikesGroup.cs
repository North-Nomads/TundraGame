using System;
using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    public class SpikesGroup : MonoBehaviour
    {
        private const int MobsMask = 1 << 8;
        private int _stunTicks;

        public int StunTicks
        {
            get => _stunTicks;
            set => _stunTicks = value;
        }

        private readonly Collider[] _colliders = new Collider[10];

        public void ApplyStunOverlappedOnMobs(float stunDamage, int stunTicks)
        {
            var touches = Physics.OverlapBoxNonAlloc(transform.position, new Vector3(1.5f, 1, .5f), _colliders,
                Quaternion.identity, MobsMask);

            for (int i = 0; i < touches; i++)
            {
                var mob = _colliders[i].GetComponent<MobBehaviour>();
                mob.AddReceivedEffects(new List<Effect> { new SpikesStunEffect(stunTicks) });
            }
        }
    }
}