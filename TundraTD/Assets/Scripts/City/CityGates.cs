﻿using Mobs;
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
            mob.KillThisMob();
        }
    } 
}
