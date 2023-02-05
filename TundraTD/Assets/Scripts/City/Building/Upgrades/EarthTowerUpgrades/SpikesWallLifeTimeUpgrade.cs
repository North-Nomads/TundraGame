using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class SpikesWallLifeTimeUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Earth;
        public int Price => 200;
        public int RequiredLevel => 2;
        public string UpgradeDescriptionText => "Increase spike wall life time by 1 second";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.SpikesWallLifeTime += 1f;
        }
    }
}
