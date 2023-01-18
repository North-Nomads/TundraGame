using System.Collections.Generic;
using Spells;

namespace City.Building.Upgrades
{
    public static class TowerUpgrades
    {
        private static readonly Upgrade[][] FireTowerUpgrades = new Upgrade[4][];

        static TowerUpgrades()
        {
            InitializeFireTowerUpgrades();
        }

        private static void InitializeFireTowerUpgrades()
        {
            FireTowerUpgrades[0] = new Upgrade[]
            {
                new WaterEnemiesDamageUpgrade(),
                new WaterEnemiesDamageUpgrade()
            };
            FireTowerUpgrades[1] = new Upgrade[]
            {
                new WaterEnemiesDamageUpgrade(),
                new WaterEnemiesDamageUpgrade(),
                new WaterEnemiesDamageUpgrade()
            };
            FireTowerUpgrades[2] = new Upgrade[]
            {
                new WaterEnemiesDamageUpgrade()
            };
            FireTowerUpgrades[3] = new Upgrade[]
            {
                new WaterEnemiesDamageUpgrade(),
                new WaterEnemiesDamageUpgrade(),
                new WaterEnemiesDamageUpgrade(),
                new WaterEnemiesDamageUpgrade()
            };
        }
        
        public static readonly Dictionary<BasicElement, Upgrade[][]> UpgradesMap 
            = new Dictionary<BasicElement, Upgrade[][]>
            {
                { BasicElement.Fire, FireTowerUpgrades },
                { BasicElement.Air , FireTowerUpgrades },
                { BasicElement.Earth, FireTowerUpgrades },
                { BasicElement.Lightning, FireTowerUpgrades },
                { BasicElement.Water , FireTowerUpgrades }
            };
    }
}