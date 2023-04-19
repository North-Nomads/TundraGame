using Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// Represents the model for the rechargeable element selection slots.
    /// </summary>
    public class ElementCharge
    {
        private const float Cooldown = 2;
        private const int MaxCharges = 6;

        private float _currentTime;

        public int CurrentCharges { get; set; } = MaxCharges;

        public float FillAmount => (CurrentCharges + _currentTime / Cooldown) / MaxCharges;

        public void DoTick(float deltaTime)
        {
            if (CurrentCharges < MaxCharges)
            {
                _currentTime += deltaTime;
                if (_currentTime >= Cooldown)
                {
                    CurrentCharges++;
                    _currentTime = 0;
                }
            }
            else
            {
                _currentTime = 0;
            }
        }

        public void ReturnCharge()
        {
            if (CurrentCharges < MaxCharges)
            {
                CurrentCharges++;
                _currentTime = 0;
            }
        }
    }
}
