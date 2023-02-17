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
        const float _DecreaseHealthBarValueBoost = 0.005f;
        const float _decreaseHealthBarValue = 0.000002f;
        private float _whiteSpriteGlowingTime = 2;

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
            float _decreaseHealthBarRate = _decreaseHealthBarValue;
            float _decreaseHealthBarUIValue;
            bool _isDamageTaken = false;
            while (true)
            {
                if (topHealthBar.fillAmount <= _targetPercent) 
                {
                    StartCoroutine(GlowWhiteSprite(_isDamageTaken));
                    _decreaseHealthBarRate = _decreaseHealthBarValue;
                    yield return null;
                }
                else
                {
                    _isDamageTaken = true;
                    _decreaseHealthBarUIValue = _decreaseHealthBarRate * _decreaseHealthBarRate;
                    topHealthBar.fillAmount -= _decreaseHealthBarUIValue;
                    StartCoroutine(GlowWhiteSprite(_isDamageTaken));
                    _isDamageTaken = false;
                    _decreaseHealthBarRate += _DecreaseHealthBarValueBoost;
                    yield return null;
                }
            }
        }
        private IEnumerator GlowWhiteSprite(bool _isDamageTaken)
        {
            if (_isDamageTaken)
            {
                takenDamageSprite.fillAmount = topHealthBar.fillAmount;
                yield return new WaitForSeconds(_whiteSpriteGlowingTime);
            }
            else
            {
                takenDamageSprite.fillAmount = 0;
            }
            yield return null;
        }
    }
}