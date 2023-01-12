using City.Building.ElementPools;
using UnityEngine;

namespace City.Building.Upgrades
{
    public class WaterEnemiesDamageUpgrade : IUpgrade
    {
        public Sprite GetIconSprite(string spriteName)
        {
            return Resources.Load<Sprite>("UpgradeIcons/Arcanist1");
        }

        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstWaterMultiplier += 1.05f;
        }
    }
}