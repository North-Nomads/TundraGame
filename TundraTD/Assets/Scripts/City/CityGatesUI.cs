﻿using UnityEngine;
using UnityEngine.UI;

namespace City
{
    /// <summary>
    /// Represents the UI behaviour of CityGates
    /// </summary>
    [RequireComponent(typeof(CityGates))]
    public class CityGatesUI : MonoBehaviour
    {
        [SerializeField] private Text influencePointsHolder;
        [SerializeField] private Text healthPointsHolder;
        private CityGates _cityGates;

        public void UpdateInfluencePointsText(string text)
        {
            influencePointsHolder.text = text;
        }
        
        public void UpdateHealthText(string text)
        {
            healthPointsHolder.text = text;
        }
        
        private void Start()
        {
            _cityGates = GetComponent<CityGates>();
            healthPointsHolder.text = _cityGates.CityGatesHealthPoints.ToString();
        }
        
    }
}