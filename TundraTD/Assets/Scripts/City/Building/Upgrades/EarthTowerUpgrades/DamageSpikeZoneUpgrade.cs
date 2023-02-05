using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class DamageSpikeZoneUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Earth;
        public int Price => 350;
        public int RequiredLevel => 3;
        public string UpgradeDescriptionText => "Increase damage when entiring enemies the spike zone by 30%";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.DamageEntireSpikeZone *= 1.3f;
        }
    }
}
