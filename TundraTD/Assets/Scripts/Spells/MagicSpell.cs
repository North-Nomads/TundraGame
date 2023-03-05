using System;
using UnityEngine;

namespace Spells
{
    public abstract class MagicSpell : MonoBehaviour
    {
        private bool _isCameraLocked;

        public bool IsCameraLocked
        {
            get => _isCameraLocked;
            protected set
            {
                SpellCameraLock(this, value);
                _isCameraLocked = value;
            }
        }

        public abstract void ExecuteSpell(RaycastHit hitInfo);
        public event EventHandler<bool> SpellCameraLock = delegate { };
    }
}