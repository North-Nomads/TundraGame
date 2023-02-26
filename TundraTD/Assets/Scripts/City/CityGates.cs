﻿using City.Building;
using Level;
using Mobs;
using Mobs.MobsBehaviour;
using Spells;
using UnityEngine;

namespace City
{
    /// <summary>
    /// Represents the behaviour of City Gates
    /// </summary>
    [RequireComponent(typeof(CityGatesUI))]
    public class CityGates : MonoBehaviour
    {
        [SerializeField] private float maxCityGatesHealthPoints;
        [SerializeField] private LevelJudge levelJudge;
        private float _currentCityGatesHealthPoints;
        private float _cityGatesHealthPercent;
        private CityGatesUI _cityGatesUI;

        private float CurrentCityGatesHealthPoints
        {
            get => _currentCityGatesHealthPoints;
            set
            {
                if (value <= 0)
                {
                    _currentCityGatesHealthPoints = 0;
                    _cityGatesHealthPercent = 0;
                    levelJudge.HandlePlayerDefeat();
                }

                _currentCityGatesHealthPoints = value;
                _cityGatesHealthPercent = _currentCityGatesHealthPoints / maxCityGatesHealthPoints;
                _cityGatesUI.UpdateHealthBar(_cityGatesHealthPercent);
            }
        }

        private void Start()
        {
            _cityGatesUI = GetComponent<CityGatesUI>();
            _currentCityGatesHealthPoints = maxCityGatesHealthPoints;
        }

        private void Update()
        {
            // HACK: made here fireball casting to test, remove later
            if (Input.GetKeyDown(KeyCode.C) && !PauseMode.IsGamePaused)
            {
                Grimoire.TurnElementsIntoSpell(new BasicElement[]
                    { BasicElement.Earth, BasicElement.Earth, BasicElement.Earth, BasicElement.None, BasicElement.None });
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(1);
            if (!other.CompareTag("Mob"))
                return;

            //Debug.Log(1);
            var mob = other.GetComponent<MobBehaviour>();
            var mobAttack = mob.GetComponent<MobModel>().CurrentMobDamage;

            CurrentCityGatesHealthPoints -= mobAttack;
            mob.HitThisMob(float.PositiveInfinity, BasicElement.None);
        }

        public void HandleWaveEnding()
        {
            Architect.RewardPlayerOnWaveEnd(_cityGatesHealthPercent);
        }
    }
}