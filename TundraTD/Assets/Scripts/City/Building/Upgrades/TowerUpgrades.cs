using System.Collections.Generic;
using Spells;

namespace City.Building.Upgrades
{
    public static class TowerUpgrades
    {
        private static readonly IUpgrade[][] FireTowerUpgrades = new IUpgrade[4][];

        static TowerUpgrades()
        {
            InitializeTowerUpgrades();
        }

        private static void InitializeTowerUpgrades()
        {
            FireTowerUpgrades[0] = new IUpgrade[]
            {
                new WaterEnemiesDamangeUpgrade(),
                new WaterEnemiesDamangeUpgrade()
            };
            FireTowerUpgrades[1] = new IUpgrade[]
            {
                new WaterEnemiesDamangeUpgrade(),
                new WaterEnemiesDamangeUpgrade(),
                new WaterEnemiesDamangeUpgrade()
            };
            FireTowerUpgrades[2] = new IUpgrade[]
            {
                new WaterEnemiesDamangeUpgrade()
            };
            FireTowerUpgrades[3] = new IUpgrade[]
            {
                new WaterEnemiesDamangeUpgrade(),
                new WaterEnemiesDamangeUpgrade(),
                new WaterEnemiesDamangeUpgrade(),
                new WaterEnemiesDamangeUpgrade()
            };
        }
        
        public static readonly Dictionary<BasicElement, IUpgrade[][]> UpgradesMap 
            = new Dictionary<BasicElement, IUpgrade[][]>
            {
                { BasicElement.Fire, FireTowerUpgrades },
                { BasicElement.Air , FireTowerUpgrades },
                { BasicElement.Earth, FireTowerUpgrades },
                { BasicElement.Lightning, FireTowerUpgrades },
                { BasicElement.Water , FireTowerUpgrades }
            };
    }
}