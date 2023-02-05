using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class AddMiniWallsUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Earth;
        public int Price => 700;
        public int RequiredLevel => 4;
        public string UpgradeDescriptionText => "Add 2 mini walls around the main wall";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.AddMiniWallsAroundWall = true;
        }
    }
}