using System;
using UnityEngine;
using UnityEngine.UI;

namespace City
{
    /// <summary>
    /// Represents the UI behaviour of CityGates
    /// </summary>
    [RequireComponent(typeof(CityGates))]
    public class CityGatesUI : MonoBehaviour
    {
        [SerializeField] private Image topHealthBar;
        [SerializeField] private Text influencePointsHolder;
        private void Start()
        {
            if (topHealthBar.sprite is null)
                throw new Exception("No sprite was assigned");
            topHealthBar.fillAmount = 1f;
        }
        
        public void UpdateInfluencePointsText(string text)
        {
            influencePointsHolder.text = text;
        }
        
        public void UpdateHealthBar(float percent)
        {
            topHealthBar.fillAmount = percent;
        }
        
        
    }
}