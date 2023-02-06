using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class DisorientingDustCloudUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 700;
        public int SpellPurchaseRequiredLevel => 4;
        public string UpgradeDescriptionText => "Create cloud of disorienting dust around the spikes";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.CloudDisorientingDust = true;
        }
    }
}