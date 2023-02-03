using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorSizeUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 200;
        public int RequiredLevel => 2;
        public string UpgradeDescriptionText => "Increse size of meteor by 20%";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.MeteorRadiusMultiplier *= 1.2f;
        }
    }
}