using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorLandingReduction : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 350;
        public int SpellPurchaseRequiredLevel => 2;
        public string UpgradeDescriptionText => "-0.4 seconds delay before meteorite impact";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.MeteorLandingReduction += 0.4f;
        }
    }
}