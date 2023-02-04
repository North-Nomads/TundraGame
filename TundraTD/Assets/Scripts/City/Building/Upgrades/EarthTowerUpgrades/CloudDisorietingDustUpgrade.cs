using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class CloudDisorietingDustUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Earth;
        public int Price => 700;
        public int RequiredLevel => 4;
        public string UpgradeDescriptionText => "Create cloud of disorienting dust around the spikes";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.CloudDisorientingDust = true;
        }
    }
}