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

        public void UpdateInfluencePointsText(string text)
        {
            influencePointsHolder.text = text;
        }
        
        public void UpdateHealthBar(float percent)
        {
            topHealthBar.fillAmount = percent;
        }
        
        private void Start()
        {
            topHealthBar.fillAmount = 1f;
        }
        
    }
}