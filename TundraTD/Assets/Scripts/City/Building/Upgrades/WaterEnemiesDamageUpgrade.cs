using City.Building.ElementPools;

namespace City.Building.Upgrades
{
    public class WaterEnemiesDamageUpgrade : IUpgrade
    {
        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstWaterMultiplier += 1.05f;
        }
    }
}