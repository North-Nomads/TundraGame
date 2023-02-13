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
        private float _currentCurrentCityGatesHealthPoints;
        private float _cityGatesHealthPercent;
        private CityGatesUI _cityGatesUI;

        private float CurrentCityGatesHealthPoints
        {
            get => _currentCurrentCityGatesHealthPoints;
            set
            {
                if (value <= 0)
                {
                    _currentCurrentCityGatesHealthPoints = 0;
                    _cityGatesHealthPercent = 0;
                    levelJudge.HandlePlayerDefeat();
                }

                _currentCurrentCityGatesHealthPoints = value;
                _cityGatesHealthPercent = _currentCurrentCityGatesHealthPoints / maxCityGatesHealthPoints;
                _cityGatesUI.UpdateHealthBar(_cityGatesHealthPercent);
            }
        }

        private void Start()
        {
            _cityGatesUI = GetComponent<CityGatesUI>();
            _currentCurrentCityGatesHealthPoints = maxCityGatesHealthPoints;
        }

        private void Update()
        {
            // HACK: made here fireball casting to test, remove later
            if (Input.GetKeyDown(KeyCode.C) && !PauseMode.IsGamePaused)
            {
                Grimoire.TurnElementsIntoSpell(new BasicElement[] { BasicElement.Fire, BasicElement.Fire, BasicElement.Fire, BasicElement.Earth, BasicElement.Earth });
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(1);
            if (!other.CompareTag("Mob"))
                return;

            Debug.Log(1);
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