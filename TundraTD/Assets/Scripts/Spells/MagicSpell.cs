using System;
using UnityEngine;

namespace Spells
{
    public abstract class MagicSpell : MonoBehaviour
    {
        private bool _isLockedCamera;

        public bool IsLockedCamera
        {
            get => _isLockedCamera;
            protected set
            {
                SpellCameraLock(this, value);
                _isLockedCamera = value;
            }
        }

        public abstract void ExecuteSpell();
        public event EventHandler<bool> SpellCameraLock = delegate { };
    }
}