using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class WaterEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 25;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Increase damage on water mobs by 5%";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstElementMultipliers[BasicElement.Water] += 0.05f;
        }
    }
}