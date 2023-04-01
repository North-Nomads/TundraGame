using Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    /// <summary>
    /// Represents the model for the rechargeable element selection slots.
    /// </summary>
    public class ElementCharge
    {
        private const float cooldown = 2;
        private const int maxCharges = 6;

        private float _currentTime;

        public int CurrentCharges { get; set; } = maxCharges;

        public float FillAmount => (CurrentCharges + _currentTime / cooldown) / maxCharges;

        public void DoTick(float deltaTime)
        {
            if (CurrentCharges < maxCharges)
            {
                _currentTime += deltaTime;
                if (_currentTime >= cooldown)
                {
                    CurrentCharges++;
                    _currentTime = 0;
                }
            }
            else _currentTime = 0;
        }
    }
}
