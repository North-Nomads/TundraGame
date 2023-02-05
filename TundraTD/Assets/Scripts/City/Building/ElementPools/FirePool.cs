using Spells;
using System.Collections.Generic;
using System;

namespace City.Building.ElementPools
{
    /// <summary>
    /// Contains all modifications that can be brought by Fire Tower upgrades
    /// </summary>
    public static class FirePool
    {
        public static Dictionary<BasicElement, float> DamageMultipliers { get; set; } = new Dictionary<BasicElement, float>();
        public static float MeteorRadiusMultiplier { get; set; } = 1f;
        public static float AfterburnDamageMultiplier { get; set; } = 1f;
        public static float MeteorLandingReduction { get; set; }
        public static bool HasLandingLavaPool { get; set; }
        public static bool HasLandingStun { get; set; }
        public static bool HasLandingImpulse { get; set; }

        static FirePool()
        {
            foreach (BasicElement element in Enum.GetValues(typeof(BasicElement)))
            {
                if (element != BasicElement.None) DamageMultipliers.Add(element, 1f);
            }
        }
    }
}