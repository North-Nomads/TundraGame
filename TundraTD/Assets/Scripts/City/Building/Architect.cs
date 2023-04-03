using Spells;
using System;
using System.Linq;
using ModulesUI.PlayerHUD;
using UnityEngine;
using UnityEngine.Analytics;

namespace City.Building
{
    /// <summary>
    /// Manages the transaction to buy towers and upgrades
    /// </summary>
    public static class Architect
    {
        private static int _influencePoints;

        public static int WaveCompletionMaxInfluencePointsAward { get; set; }
        public static int WaveCompletionMinInfluencePointsAward { get; set; }
        public static Transform CanvasesHierarchyParent { get; set; }
        public static CityGatesUI InfluencePointsHolder { get; set; }

        private static int InfluencePoints
        {
            get => _influencePoints;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Influence Points now  below zero");
                InfluencePointsHolder.UpdateInfluencePointsText(value.ToString());
                _influencePoints = value;
            }
        }

        public static void DEBUG_GetStartPoints()
        {
            InfluencePoints = 10000;
        }

        public static void RewardPlayerOnWaveEnd(float cityGatesHPPercent)
        {
            InfluencePoints += WaveCompletionMinInfluencePointsAward + (int)(WaveCompletionMaxInfluencePointsAward * cityGatesHPPercent);
        }
    }
}