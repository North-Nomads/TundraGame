using UnityEngine;

namespace ModulesUI
{
    public abstract class TundraCanvas : MonoBehaviour
    {
        public abstract CanvasGroup CanvasGroup { get; }
        public abstract CanvasGroup BlockList { get; }

        public virtual void ExecuteOnOpening()
        {
            gameObject.SetActive(true);
        }

        public virtual void ExecuteOnClosing()
        {
            gameObject.SetActive(false);
        }
    }
}
