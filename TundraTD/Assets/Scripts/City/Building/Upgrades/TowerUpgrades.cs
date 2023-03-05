using Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace City.Building.Upgrades
{
    public static class TowerUpgrades
    {
        public static Dictionary<BasicElement, IUpgrade[,]> UpgradesMap { get; } = new Dictionary<BasicElement, IUpgrade[,]>();

        static TowerUpgrades()
        {
            InitializeFireTowerUpgrades();
        }

        private static void InitializeFireTowerUpgrades()
        {
            foreach (BasicElement element in Enum.GetValues(typeof(BasicElement)))
                if (element != BasicElement.None)
                    UpgradesMap.Add(element, new IUpgrade[3, 2]);

            var upgradeClasses = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(IUpgrade).IsAssignableFrom(x) && !x.IsAbstract);

            foreach (var upgradeClass in upgradeClasses)
            {
                if (!(Activator.CreateInstance(upgradeClass) is IUpgrade upgrade))
                    throw new Exception("Upgrade is null");

                var elementUpgrades = UpgradesMap[upgrade.UpgradeTowerElement];
                if (elementUpgrades[upgrade.SpellPurchaseRequiredLevel - 1, 0] is null)
                    elementUpgrades[upgrade.SpellPurchaseRequiredLevel - 1, 0] = upgrade;
                else
                    elementUpgrades[upgrade.SpellPurchaseRequiredLevel - 1, 1] = upgrade;
            }
        }
    }
}