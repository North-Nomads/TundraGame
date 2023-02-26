using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorLandingImpulseUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 700;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "When a meteor lands, all opponents are thrown away from the epicenter";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.HasLandingImpulse = true;
        }
    }
}