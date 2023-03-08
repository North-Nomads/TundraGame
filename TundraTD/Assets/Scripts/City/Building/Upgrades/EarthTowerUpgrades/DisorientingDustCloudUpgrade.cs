using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class DisorientingDustCloudUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 200;
        public int SpellPurchaseRequiredLevel => 2;
        public string UpgradeDescriptionText => "Creates a dust cloud on spikes raising and confuses the enemies";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Earth/Priest13");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.HasDustCloud = true;
        }
    }
}