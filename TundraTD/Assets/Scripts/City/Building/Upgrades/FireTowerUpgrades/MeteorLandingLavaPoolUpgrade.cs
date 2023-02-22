using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorLandingLavaPoolUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 350;
        public int SpellPurchaseRequiredLevel => 2;
        public string UpgradeDescriptionText => "Meteor leaves behind a lava crater";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.HasLandingLavaPool = true;
        }
    }
}