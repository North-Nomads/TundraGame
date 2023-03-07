using UnityEngine;

namespace Spells
{
    public abstract class MagicSpell : MonoBehaviour
    {
        public abstract void ExecuteSpell(RaycastHit hitInfo);
    }
}