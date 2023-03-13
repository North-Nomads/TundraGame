using System;
using UnityEngine;

namespace ModulesUI
{
    public abstract class TundraCanvas : MonoBehaviour
    {
        public abstract CanvasGroup CanvasGroup { get; }
        public abstract CanvasGroup BlockList { get; }

        public abstract void ExecuteOnOpening();
        public abstract void ExecuteOnClosing();
    }
}
