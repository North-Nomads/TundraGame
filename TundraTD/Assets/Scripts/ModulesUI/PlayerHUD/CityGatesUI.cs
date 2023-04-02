using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.PlayerHUD
{
    /// <summary>
    /// Represents the UI behaviour of CityGates
    /// </summary>
    public class CityGatesUI : TundraCanvas
    {
        public override CanvasGroup CanvasGroup => CanvasGroup.City;
        public override CanvasGroup BlockList => CanvasGroup.None;
        
        private const float DecreaseHealthBarValueBoost = 0.005f;
        private const float DecreaseHealthBarValue = 0.000002f;
        private const float WhiteSpriteGlowingTime = 2;
        
        [SerializeField] private Image topHealthBar;
        [SerializeField] private Image takenDamageSprite;
        [SerializeField] private Text influencePointsHolder;
        private float _targetPercent;

        private void Start()
        {
            if (topHealthBar.sprite == null)
                throw new ArgumentNullException("topHealthBar.sprite", "Value of HealthBar sprite was not assigned");
            
            UIToggle.AllCanvases.Add(this);
            
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

        private void OnEnable()
        {
            StartCoroutine(PlayHealthBarAnimation());
        }

        private IEnumerator PlayHealthBarAnimation()
        {
            var decreaseHealthBarRate = DecreaseHealthBarValue;
            while (true)
            {
                if (topHealthBar.fillAmount <= _targetPercent) 
                {
                    StartCoroutine(GlowWhiteSprite(false));
                    decreaseHealthBarRate = DecreaseHealthBarValue;
                    yield return null;
                }
                else
                {
                    var decreaseHealthBarUIValue = decreaseHealthBarRate * decreaseHealthBarRate;
                    topHealthBar.fillAmount -= decreaseHealthBarUIValue;
                    StartCoroutine(GlowWhiteSprite(true));
                    decreaseHealthBarRate += DecreaseHealthBarValueBoost;
                    yield return null;
                }
            }
        }
        
        private IEnumerator GlowWhiteSprite(bool isDamageTaken)
        {
            if (isDamageTaken)
            {
                takenDamageSprite.fillAmount = topHealthBar.fillAmount;
                yield return new WaitForSeconds(WhiteSpriteGlowingTime);
            }
            else
            {
                takenDamageSprite.fillAmount = 0;
            }
            yield return null;
        }
    }
}