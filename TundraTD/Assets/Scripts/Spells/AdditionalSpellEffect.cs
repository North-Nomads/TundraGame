using Mobs.MobEffects;
using Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    /// <summary>
    /// Represents the additional effect that is used when player fully filled all element slots.
    /// </summary>
    public class AdditionalSpellEffect : ScriptableObject
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private GameObject vfxInstance;
        [SerializeField] private Effect effectToCast;

        public BasicElement Element => element;

        public GameObject VfxInstance => vfxInstance;

        public Effect Effect => effectToCast;
    }
}
