using Spells;
using System.Collections.Generic;
using System;

namespace City.Building.ElementPools
{
    /// <summary>
    /// Contains all modifications that can be brought by Earth Tower upgrades
    /// </summary>
    public static class EarthPool
    {
        public static Dictionary<BasicElement, float> DamageAgainstElementMultipliers { get; set; } = new Dictionary<BasicElement, float>();
        public static float SlowDownEnemiesTime { get; set; } = 2f;
        public static float SpikesWallLifeTime { get; set; } = 4f;
        public static float DamageEntireSpikeZone { get; set; } = 30f;
        public static float TimeStunEnemies { get; set; } = 0.7f;
        public static bool AddMiniWallsAroundWall { get; set; }
        public static bool CloudDisorientingDust { get; set; }
   

        static EarthPool()
        {
            foreach (BasicElement element in Enum.GetValues(typeof(BasicElement)))
            {
                if (element != BasicElement.None) DamageAgainstElementMultipliers.Add(element, 1f);
            }
        }
    }
}