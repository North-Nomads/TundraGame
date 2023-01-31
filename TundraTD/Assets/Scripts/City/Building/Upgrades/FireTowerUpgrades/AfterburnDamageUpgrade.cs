using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class AfterburnDamageUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 200;
        public int RequiredLevel => 2;
        public string UpgradeDescriptionText => "Increse afterburn damage by 7%";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.AfterburnDamageMultiplier *= 1.07f;
        }
    }
}