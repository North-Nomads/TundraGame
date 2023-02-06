using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class AirEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Earth;
        public int Price => 25;
        public int RequiredLevel => 1;
        public string UpgradeDescriptionText => "Increase damege on air mobs by 5%";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.DamageAgainstElementMultipliers[BasicElement.Air] *= 1.05f;
        }
    }
}
