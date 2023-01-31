using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mobs
{
    public class MobWaveBar : MonoBehaviour
    {
        private Image _waveFiller;
        private float _mobsWaveTotalScore;
        private float _mobWaveCurrentScore;

        private float MobWaveCurrentScore
        {
            get => _mobWaveCurrentScore;
            set
            {
                if (value < 0)
                    throw new Exception("Value below zero");
                _mobWaveCurrentScore = value;
            }
        }

        private void Start()
        {
            _waveFiller = GetComponent<Image>();
        }

        private void UpdateFillerStatus()
        {
            _waveFiller.fillAmount = MobWaveCurrentScore / _mobsWaveTotalScore;
        }

        public void ResetValuesOnWaveStarts(float totalScore)
        {
            _mobsWaveTotalScore = totalScore;
            MobWaveCurrentScore = totalScore;
            UpdateFillerStatus();
        }

        public void DecreaseCurrentMobScore(float score)
        {
            MobWaveCurrentScore -= score;
            UpdateFillerStatus();
        }
    }
}