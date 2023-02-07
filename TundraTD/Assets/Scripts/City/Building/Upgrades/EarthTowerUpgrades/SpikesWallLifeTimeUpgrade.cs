using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class SpikesWallLifeTimeUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 200;
        public int SpellPurchaseRequiredLevel => 2;
        public string UpgradeDescriptionText => "Increase spike wall life time by 1 second";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.SpikesWallLifeTime++;
        }
    }
}