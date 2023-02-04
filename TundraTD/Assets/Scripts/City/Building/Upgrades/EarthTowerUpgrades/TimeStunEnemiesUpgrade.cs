using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class TimeStunEnemiesUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Earth;
        public int Price => 350;
        public int RequiredLevel => 3;
        public string UpgradeDescriptionText => "Increase time stun of enemies by 0.5 second";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.TimeStunEnemies += 0.5f;
        }
    }
}

