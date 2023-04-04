using City.Building;
using Level;
using Mobs.MobsBehaviour;
using ModulesUI.MagicScreen;
using ModulesUI.PlayerHUD;
using Spells;
using UnityEngine;

namespace City
{
    /// <summary>
    /// Represents the behaviour of City Gates
    /// </summary>
    public class CityGates : MonoBehaviour
    {
        [SerializeField] private CityGatesUI cityGatesUI;
        [SerializeField] private float maxCityGatesHealthPoints;
        [SerializeField] private LevelJudge levelJudge;
        private float _currentCityGatesHealthPoints;
        private float _cityGatesHealthPercent;
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
                cityGatesUI.UpdateHealthBar(_cityGatesHealthPercent);
            }
        }

        private void Start()
        {
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
                // TODO
                SpellCaster.PerformDebugCast();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;
           
            var mob = other.GetComponent<MobBehaviour>();
            var mobAttack = mob.MobModel.CurrentMobDamage;
            CurrentCityGatesHealthPoints -= mobAttack;
            mob.HitThisMob(float.PositiveInfinity, BasicElement.None, "City.Gates");
            //_animator.SetTrigger("DamageTrigger");
        }

        public void HandleWaveEnding()
        {
            Architect.RewardPlayerOnWaveEnd(_cityGatesHealthPercent);
        }
    }
}