namespace City.Building.ElementPools
{
    /// <summary>
    /// Contains all modifications that can be brought by Earth Tower upgrades
    /// </summary>
    public static class EarthPool
    {
        public static bool HasDustCloud { get; set; } 
        public static bool HasExplosivePebbles { get; set; }
        public static bool HasTermites { get; set; }
        public static bool HasAdditionalWalls { get; set; } 
        public static bool HasSolidWalls { get; set; }
        public static bool HasFloatingStones { get; set; }
    }
}