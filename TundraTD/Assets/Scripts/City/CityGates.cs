using City.Building;
using Level;
using Mobs;
using Spells;
using Mobs.MobsBehaviour;
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
        private CityGatesUI _cityGatesUI;
        private Grimoire _grimoire;

        public float CityGatesHealthPoints
        {
            get => maxCityGatesHealthPoints;
            private set
            {
                if (value <= 0)
                {
                    maxCityGatesHealthPoints = 0;
                    levelJudge.HandlePlayerDefeat();
                }
                maxCityGatesHealthPoints = value;
                _cityGatesUI.UpdateHealthText(value.ToString());
            }
        }

        private void Start()
        {
            _cityGatesUI = GetComponent<CityGatesUI>();
            _grimoire = GetComponent<Grimoire>();
            _currentCityGatesHealthPoints = maxCityGatesHealthPoints;
        }

        private void Update()
        {
            // HACK: made here fireball casting to test, remove later
            if (Input.GetKeyDown(KeyCode.C))
            {
                _grimoire.TurnElementsIntoSpell(new BasicElement[] { BasicElement.Fire, BasicElement.Fire, BasicElement.Fire, BasicElement.Earth, BasicElement.Earth });
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            var mob = other.GetComponent<MobBehaviour>();
            var mobAttack = mob.GetComponent<MobModel>().CurrentMobDamage;

            CityGatesHealthPoints -= mobAttack;
            mob.KillThisMob();
        }

        public void HandleWaveEnding()
        {
            Architect.RewardPlayerOnWaveEnd(_currentCityGatesHealthPoints / maxCityGatesHealthPoints);
        }
    }
}