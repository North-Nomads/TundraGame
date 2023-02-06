using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class FireEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 30;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Increase damege on fire mobs by 5%";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstElementMultipliers[BasicElement.Fire] += 0.05f;
        }
    }
}