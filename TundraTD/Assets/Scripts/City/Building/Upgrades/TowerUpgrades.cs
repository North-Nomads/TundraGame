using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spells;

namespace City.Building.Upgrades
{
    public static class TowerUpgrades
    {
        private static readonly Dictionary<BasicElement, IUpgrade[,]> upgradesMap = new Dictionary<BasicElement, IUpgrade[,]>();

        public static Dictionary<BasicElement, IUpgrade[,]> UpgradesMap => upgradesMap;


        static TowerUpgrades()
        {
            InitializeFireTowerUpgrades();
        }

        private static void InitializeFireTowerUpgrades()
        {
            foreach (BasicElement element in Enum.GetValues(typeof(BasicElement)))
                if (element != BasicElement.None)
                    upgradesMap.Add(element, new IUpgrade[4, 2]);
            
            var upgradeClasses = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(IUpgrade).IsAssignableFrom(x) && !x.IsAbstract);

            foreach (var upgradeClass in upgradeClasses)
            {
                if (!(Activator.CreateInstance(upgradeClass) is IUpgrade upgrade))
                    throw new Exception("Upgrade is null");

                var elementUpgrades = upgradesMap[upgrade.Element];
                if (elementUpgrades[upgrade.RequiredLevel - 1, 0] is null)
                    elementUpgrades[upgrade.RequiredLevel - 1, 0] = upgrade;
                else
                    elementUpgrades[upgrade.RequiredLevel - 1, 1] = upgrade;
            }
        }
        
        
    }
}