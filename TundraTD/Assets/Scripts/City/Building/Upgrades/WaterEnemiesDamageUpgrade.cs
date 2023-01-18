using City.Building.ElementPools;
using UnityEngine;

namespace City.Building.Upgrades
{
    public class WaterEnemiesDamageUpgrade : Upgrade
    {
        public override int Price => 80;
        public override int RequiredLevel => 1;
        public override Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public override void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstWaterMultiplier += 1.05f;
        }
    }
}