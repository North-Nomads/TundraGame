using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class WaterEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 25;
        public int RequiredLevel => 1;
        public string UpgradeDescriptionText => "MY TEXT";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstWaterMultiplier += 1.05f;
        }
    }
}