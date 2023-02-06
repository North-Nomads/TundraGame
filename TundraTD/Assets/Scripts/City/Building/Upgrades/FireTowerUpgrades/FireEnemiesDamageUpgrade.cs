using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class FireEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 30;
        public int RequiredLevel => 1;
        public string UpgradeDescriptionText => "Increase damege on fire mobs by 5%";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstElementMultipliers[BasicElement.Fire] += 0.05f;
        }
    }
}