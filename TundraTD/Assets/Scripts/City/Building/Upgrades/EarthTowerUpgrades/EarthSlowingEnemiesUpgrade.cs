using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class EarthSlowingEnemiesUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 200;
        public int SpellPurchaseRequiredLevel => 2;
        public string UpgradeDescriptionText => "Slows down enemies by 10%";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.SlowDownEnemiesTime *= 1.1f;
        }
    }
}