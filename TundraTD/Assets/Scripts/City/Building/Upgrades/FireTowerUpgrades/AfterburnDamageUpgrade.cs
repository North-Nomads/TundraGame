using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class AfterburnDamageUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 200;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Increse afterburn damage by 7%";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.AfterburnDamageMultiplier *= 1.07f;
        }
    }
}