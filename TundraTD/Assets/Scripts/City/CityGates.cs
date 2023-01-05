using Mobs;
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
        [SerializeField] private float cityGatesHealthPoints;
        private CityGatesUI _cityGatesUI;

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
            _cityGatesUI = GetComponent< CityGatesUI>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;
            
            var mob = other.GetComponent<MobBehaviour>();
            var mobAttack = mob.GetComponent<MobModel>().CurrentMobDamage;

            CityGatesHealthPoints -= mobAttack;
            // HACK: made here fireball casting to test, remove later
            Grimoire.TurnElementsIntoSpell(new BasicElement[] { BasicElement.Fire, BasicElement.Fire, BasicElement.Fire, BasicElement.Earth, BasicElement.Earth }).ExecuteSpell();
            mob.KillThisMob();
        }
    } 
}
