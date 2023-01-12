namespace City.Building.ElementPools
{
    /// <summary>
    /// Contains all modifications that can be brought by Fire Tower upgrades
    /// </summary>
    public static class FirePool
    {
        public static float DamageAgainstWaterMultiplier { get; set; }
        public static float GeneralizedFireDamageMultiplier { get; set; }
        public static float MeteoriteRadiusMultiplier { get; set; }
        public static float AfterburningDamageMultiplier { get; set; }
        public static float MeteoriteLandingDelayReductionInSeconds { get; set; }
        public static bool MeteoriteLavaPoolAfterLanding { get; set; }
        public static bool MeteoriteStunOnLanding { get; set; }
        public static bool MeteoriteImpulseOnLanding { get; set; }
    }
}