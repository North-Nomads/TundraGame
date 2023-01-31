using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mobs
{
    public class MobWaveBar : MonoBehaviour
    {
        private Image _waveFiller;
        private int _mobsWaveTotalScore;
        private int _mobWaveCurrentScore;

        public int MobWaveCurrentScore
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

        public void ResetValuesOnWaveStarts(int totalScore)
        {
            Debug.Log(totalScore);
            _mobsWaveTotalScore = totalScore;
            _mobWaveCurrentScore = totalScore;
            UpdateFillerStatus();
        }

        public void DecreaseCurrentMobScore(int score)
        {
            _mobWaveCurrentScore -= score;
            UpdateFillerStatus();
        }
    }
}