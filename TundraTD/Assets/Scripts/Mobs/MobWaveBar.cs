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

        public float MobWaveCurrentScore
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
            _waveFiller.fillAmount = _mobWaveCurrentScore / _mobsWaveTotalScore;
        }

        public void ResetValuesOnWaveStarts(float totalScore)
        {
            Debug.Log(totalScore);
            _mobsWaveTotalScore = totalScore;
            _mobWaveCurrentScore = totalScore;
            UpdateFillerStatus();
        }

        public void DecreaseCurrentMobScore(float score)
        {
            _mobWaveCurrentScore -= score;
            UpdateFillerStatus();
            Debug.Log(_mobsWaveTotalScore);
        }
    }
}