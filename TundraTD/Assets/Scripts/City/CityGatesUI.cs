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
        [SerializeField] private Text influencePointsHolder;
        [SerializeField] private Text healthPointsHolder;
        [SerializeField] private Text resultOnEndScreen;
        [SerializeField] private Text endscreenPlayButtonText;
        [SerializeField] private Canvas endScreen;
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
        /// <summary>
        /// Displays Victory or Loss screen
        /// </summary>
        public void DisplayEndScreed(bool victorious)
        {
            endscreenPlayButtonText.text = victorious ? "Следующий уровень" : "Повторить";
            resultOnEndScreen.text = victorious ? "Победа" : "Поражение";
            endScreen.enabled = true;
        }

    }
}