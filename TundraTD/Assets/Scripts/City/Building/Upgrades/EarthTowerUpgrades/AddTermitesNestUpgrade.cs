using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class AddTermitesNestUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 200;
        public int SpellPurchaseRequiredLevel => 2;
        public string UpgradeDescriptionText => "Termites are biting the mobs stuck on the spikes";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Earth/Geomancer5");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.HasTermites = true;
        }
    }
}