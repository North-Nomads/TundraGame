using System;
using UnityEngine;

namespace Spells
{
    public abstract class MagicSpell : MonoBehaviour
    {
        public abstract void ExecuteSpell();
        public abstract event EventHandler SpellCameraLock; 
    }
}