using System.Collections.Generic;
using City.Building;
using Level;
using Mobs.MobsBehaviour;
using Mobs.MobsBehaviour.Ironclad;
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
        private Animator _animator;

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
            _animator = GetComponent<Animator>();

        }

        private void Update()
        {
            // HACK: made here fireball casting to test, remove later
            if (Input.GetKey(KeyCode.C) && !PauseMode.IsGamePaused)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    Grimoire.TurnElementsIntoSpell(new List<BasicElement> { BasicElement.Fire, BasicElement.Fire, BasicElement.Fire, BasicElement.Earth, BasicElement.Earth });
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    Grimoire.TurnElementsIntoSpell(new List<BasicElement> { BasicElement.Water, BasicElement.Water, BasicElement.Water, BasicElement.Earth, BasicElement.Earth });
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    Grimoire.TurnElementsIntoSpell(new List<BasicElement> { BasicElement.Earth, BasicElement.Earth, BasicElement.Earth, BasicElement.Earth, BasicElement.Earth });
                if (Input.GetKeyDown(KeyCode.Alpha4))
                    Grimoire.TurnElementsIntoSpell(new List<BasicElement> { BasicElement.Lightning, BasicElement.Lightning, BasicElement.Lightning, BasicElement.Earth, BasicElement.Earth });
                if (Input.GetKeyDown(KeyCode.Alpha5))
                    Grimoire.TurnElementsIntoSpell(new List<BasicElement> { BasicElement.Air, BasicElement.Air, BasicElement.Air, BasicElement.Earth, BasicElement.Earth });
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;
           
            var mob = other.GetComponent<MobBehaviour>();
            var mobAttack = mob.MobModel.CurrentMobDamage;

            if (mob.GetComponent<IroncladBehaviour>() != null)
            {
                
            }

            CurrentCityGatesHealthPoints -= mobAttack;
            mob.HitThisMob(float.PositiveInfinity, BasicElement.None);

            _animator.SetTrigger("DamageTrigger");
            
        }

        public void HandleWaveEnding()
        {
            Architect.RewardPlayerOnWaveEnd(_cityGatesHealthPercent);
        }
    }
}