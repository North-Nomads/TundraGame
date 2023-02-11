using System;
using System.Collections;
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
        private float _targetPercent;

        private void Start()
        {

            if (topHealthBar.sprite == null)
                throw new ArgumentNullException("topHealthBar sprite", "Value of healthbar sprite was not assigned");
            topHealthBar.fillAmount = 1f;
            _targetPercent = topHealthBar.fillAmount;
            StartCoroutine(PlayHealthBarAnimation());
        }

        public void UpdateInfluencePointsText(string text)
        {
            influencePointsHolder.text = text;
        }

        public void UpdateHealthBar(float percent)
        {
            _targetPercent = percent;
        }
        private IEnumerator PlayHealthBarAnimation()
        {
            while (true)
            {
                if (topHealthBar.fillAmount <= _targetPercent) yield return null;
                else
                {
                    topHealthBar.fillAmount -= 0.001f;
                    yield return null;
                }
            }
        }
    }
}