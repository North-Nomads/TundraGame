using System.Collections.Generic;
using City.Building;
using Level;
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
        private Animator _animator;
        
        // Const is used by raycast in update which is debug purpose only 
        private const int PlaceableLayer = 1 << 11 | 1 << 10;

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
                var rayEnd = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(rayEnd, out var hitInfo, float.PositiveInfinity, PlaceableLayer)) return;
                
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    Grimoire.TurnElementsIntoSpell(hitInfo, BasicElement.Fire);
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    Grimoire.TurnElementsIntoSpell(hitInfo, BasicElement.Air);
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    Grimoire.TurnElementsIntoSpell(hitInfo, BasicElement.Water);
                if (Input.GetKeyDown(KeyCode.Alpha4))
                    Grimoire.TurnElementsIntoSpell(hitInfo, BasicElement.Lightning);
                if (Input.GetKeyDown(KeyCode.Alpha5))
                    Grimoire.TurnElementsIntoSpell(hitInfo, BasicElement.Earth);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;
           
            var mob = other.GetComponent<MobBehaviour>();
            var mobAttack = mob.MobModel.CurrentMobDamage;
            
            Debug.Log($"{mob.name} attacked tower with {mobAttack} damage");
            CurrentCityGatesHealthPoints -= mobAttack;
            mob.HitThisMob(float.PositiveInfinity, BasicElement.None, "City.Gates");
            _animator.SetTrigger("DamageTrigger");
        }

        public void HandleWaveEnding()
        {
            Architect.RewardPlayerOnWaveEnd(_cityGatesHealthPercent);
        }
    }
}