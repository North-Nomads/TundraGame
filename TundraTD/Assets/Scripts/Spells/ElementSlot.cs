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
    public class ElementSlot
    {
        [SerializeField] private float cooldown;
        [SerializeField] private BasicElement selectionElement;
        [SerializeField] private int maximalAmount;

        private float _currentTime;

        public BasicElement SelectionElement => selectionElement;

        public int CurrentAmount { get; set; }

        public void DoTick(float deltaTime)
        {
            if (CurrentAmount < maximalAmount)
            {
                _currentTime += deltaTime;
                if (_currentTime >= cooldown)
                {
                    CurrentAmount++;
                    _currentTime = 0;
                }
            }
            else _currentTime = 0;
        }
    }
}
