using Assets.Scripts.Spells;
using Mobs.MobsBehaviour;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Spells
{
    public abstract class MagicSpell : MonoBehaviour
    {
        private static Dictionary<BasicElement, MagicSpell> _prefabs;

        public static Dictionary<BasicElement, AdditionalSpellEffect> AdditionalSpellEffects { get; set; }

        public static event EventHandler<SpellCastInfo> SpellCast = delegate { };

        public AdditionalSpellEffect AdditionalEffect { get; private set; }

        public abstract BasicElement Element { get; }
        
        public virtual bool Cast(RaycastHit hitInfo)
        {
            var info = new SpellCastInfo(hitInfo);
            SpellCast(this, info);
            if (info.Cancel)
            {
                Destroy(gameObject);
                return false;
            }
            ExecuteSpell(hitInfo);
            return true;
        }

        public abstract void ExecuteSpell(RaycastHit hitInfo);

        public static MagicSpell InstantiateSpellPrefab(BasicElement basis, BasicElement addition = BasicElement.None)
        {
            if (!_prefabs.ContainsKey(basis))
                return null;
            var spell = Instantiate(_prefabs[basis]);
            if (addition != BasicElement.None && AdditionalSpellEffects.ContainsKey(addition))
            {
                spell.AdditionalEffect = AdditionalSpellEffects[addition];
            }
            return spell;
        }

        public static bool CanMakeSpell(BasicElement basis)
        {
            return _prefabs.ContainsKey(basis);
        }

        /// <summary>
        /// Sets spell prefabs into the internal dictionary based on their basic elements.
        /// </summary>
        /// <param name="prefabs">Prefabs array that can be set up in the Editor.</param>
        public static void SetSpellPrefabs(MagicSpell[] prefabs, AdditionalSpellEffect[] spellEffects)
        {
            _prefabs = prefabs.Where(x => x != null).ToDictionary(x => x.Element, y => y);
            AdditionalSpellEffects = spellEffects.Where(x => x != null).ToDictionary(x => x.Element, y => y);
        }

        protected void DisableEmissionOnChildren()
        {
            foreach (var system in GetComponentsInChildren<ParticleSystem>())
            {
                var emission = system.emission;
                emission.enabled = false;
            }
        }

        protected void ApplyAdditionalEffects(params MobBehaviour[] mobs)
        {
            if (AdditionalEffect == null) return;
            foreach (var mob in mobs)
            {
                mob?.AddSingleEffect(AdditionalEffect.EffectToApply);
            }
        }

        public class SpellCastInfo : CancelEventArgs
        {
            public RaycastHit HitInfo { get; }

            public SpellCastInfo(RaycastHit hit)
            {
                HitInfo = hit;
            }
        }
    }
}