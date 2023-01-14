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
        [SerializeField] private float cityGatesHealthPoints;
        private CityGatesUI _cityGatesUI;
        private Grimoire _grimoire;

        public float CityGatesHealthPoints
        {
            get => cityGatesHealthPoints;
            private set
            {
                if (value <= 0)
                    cityGatesHealthPoints = 0;
                cityGatesHealthPoints = value;
                _cityGatesUI.UpdateHealthText(value.ToString());
            }
        }

        private void Start()
        {
            _cityGatesUI = GetComponent<CityGatesUI>();
            _grimoire = GetComponent<Grimoire>();
        }

        private void Update()
        {
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
            // HACK: made here fireball casting to test, remove later
            _grimoire.TurnElementsIntoSpell(new BasicElement[] { BasicElement.Fire, BasicElement.Fire, BasicElement.Fire, BasicElement.Earth, BasicElement.Earth });
            //mob.KillThisMob();
        }
    }
}