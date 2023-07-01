using Level;
using Mobs.MobEffects;
using Spells;
using System;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    /// <summary>
    /// Represents the additional effect that is used when player fully filled all element slots.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "Additional Effect", menuName = "Spells/Additional effect")]
    public class AdditionalSpellEffect : ScriptableObject
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private GameObject vfxInstance;
        [SerializeField] private float effectMultiplier;
        [SerializeField] private float effectDuration;

        public Effect EffectToApply => element.ToEffect(effectMultiplier, effectDuration);

        public BasicElement Element => element;

        public GameObject VfxInstance => vfxInstance;
    }

    internal static class ElementEffectHelper
    {
        public static Effect ToEffect(this BasicElement element, float damage, float time)
        {
            switch (element)
            {
                case BasicElement.Fire:
                    return new BurningEffect(damage, time.SecondsToTicks());

                case BasicElement.Air:
                    return new InspirationEffect();

                case BasicElement.Earth:
                    return new StunEffect(time.SecondsToTicks());

                case BasicElement.Water:
                    return new WetEffect(time.SecondsToTicks());

                case BasicElement.Lightning:
                    //return new PolarizationEffect();
                    return new StunEffect(time.SecondsToTicks());

                default:
                    throw new ArgumentException("Wrong effect type.");
            }
        }
    }
}