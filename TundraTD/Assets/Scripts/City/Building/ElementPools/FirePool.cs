using Spells;
using System;
using System.Collections.Generic;

namespace City.Building.ElementPools
{
    /// <summary>
    /// Contains all modifications that can be brought by Fire Tower upgrades
    /// </summary>
    public static class FirePool
    {
        public static float MeteorRadiusMultiplier { get; set; } = 1f;
        public static float AfterburnDamageMultiplier { get; set; } = 1f;
        public static float MeteorLandingReduction { get; set; }
        public static bool HasLandingLavaPool { get; set; }

        // New skills

        public static bool HasWaterResist { get; set; }

        public static bool HasLandingStun { get; set; }

        public static bool HasLavaPool { get; set; }

        public static bool HasLandingImpulse { get; set; }

        public static bool HasLandingTraps { get; set; }

        public static bool HasLandingGhost { get; set; }
    }
}