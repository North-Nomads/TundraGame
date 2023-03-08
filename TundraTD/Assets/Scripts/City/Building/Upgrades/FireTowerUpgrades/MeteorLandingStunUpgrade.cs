using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorLandingStunUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 0;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "When a meteor lands, a deafening wave appears";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Fire/Barbarian14");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.HasLandingStun = true;
        }
    }
}