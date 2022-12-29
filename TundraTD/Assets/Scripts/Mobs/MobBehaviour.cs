using System;
using UnityEngine;

namespace Mobs
{
    /// <summary>
    /// An abstract class that represents the basic behavior of every mob on the map
    /// </summary>
    public abstract class MobBehaviour : MonoBehaviour
    {
        public abstract void HandleAppliedEffects();
        public abstract void ApplyReceivedEffects();
        public abstract void MoveTowards(Vector3 point);
        public abstract void KillThisMob();
    }
}
