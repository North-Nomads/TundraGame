using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class MakeWallsSolidUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 100;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Spikes turn into a solid wall that can't be walked through";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Earth/Brawler18");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.HasSolidWalls = true;
        }
    }
}