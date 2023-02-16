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
        [SerializeField] private Image takenDamageSprite;
        [SerializeField] private Text influencePointsHolder;
        private float _targetPercent;

        private void Start()
        {

            if (topHealthBar.sprite == null)
                throw new ArgumentNullException("topHealthBar sprite", "Value of healthbar sprite was not assigned");
            topHealthBar.fillAmount = 1f;
            takenDamageSprite.fillAmount = 0;
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
            float _decreaseHealthBarRate = 0.000002f;
            float a;
            bool flag = false;
            while (true)
            {
                if (topHealthBar.fillAmount <= _targetPercent) 
                {
                    StartCoroutine(onemore(flag));
                    _decreaseHealthBarRate = 0.000002f;
                    yield return null;
                }
                else
                {
                    flag = true;
                    a = _decreaseHealthBarRate * _decreaseHealthBarRate;
                    topHealthBar.fillAmount -= a;
                    StartCoroutine(onemore(flag));
                    flag = false;
                    _decreaseHealthBarRate += 0.005f;
                    yield return null;
                }
            }
        }
        private IEnumerator onemore(bool flag)
        {
            if (flag)
            {
                takenDamageSprite.fillAmount = topHealthBar.fillAmount;
                yield return new WaitForSeconds(2);
            }
            else
            {
                takenDamageSprite.fillAmount = 0;
            }
            yield return null;
        }
    }
}