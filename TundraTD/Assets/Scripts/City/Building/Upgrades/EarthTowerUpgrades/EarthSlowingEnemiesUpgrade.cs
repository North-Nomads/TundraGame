using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class EarthSlowingEnemiesUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Earth;
        public int Price => 200;
        public int RequiredLevel => 2;
        public string UpgradeDescriptionText => "Slows down enemies by 10%";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.SlowDownEnemiesTime *= 1.1f;
        }
    }
}