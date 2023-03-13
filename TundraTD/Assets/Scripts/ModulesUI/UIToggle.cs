using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

namespace ModulesUI
{
    public static class UIToggle
    {
        public static readonly List<TundraCanvas> AllCanvases;
        private static readonly List<TundraCanvas> OpenedCanvases;
        private static CanvasGroup _blockedGroups;

        static UIToggle()
        {
            OpenedCanvases = new List<TundraCanvas>();
            AllCanvases = new List<TundraCanvas>();
        }

        public static void TryOpenCanvas(TundraCanvas canvas)
        {
            if (_blockedGroups.HasFlag(canvas.CanvasGroup))
                return;

            _blockedGroups |= canvas.BlockList;
            OpenedCanvases.Add(canvas);
            canvas.ExecuteOnOpening();
        }

        public static void DisableBlockedCanvases()
        {
            foreach (var canvas in AllCanvases.Where(canvas => _blockedGroups.HasFlag(canvas.BlockList)))
                canvas.ExecuteOnClosing();
        }

        public static void HandleCanvasClosing(TundraCanvas canvas)
        {
            // Clear all groups 
            foreach (CanvasGroup group in Enum.GetValues(typeof(CanvasGroup)))
                _blockedGroups &= ~group;
            
            // Then remove this canvas
            OpenedCanvases.Remove(canvas);
            canvas.ExecuteOnClosing();
            
            // And add OpenCanvases groups again. Now without the removed one
            foreach (var openedCanvas in OpenedCanvases)
                _blockedGroups |= ~openedCanvas.CanvasGroup;
        }
    }
    
    [Flags]
    public enum CanvasGroup
    {
        None = 0,
        MagicHUD = 1,
        Building = 1 << 1,
        Portal = 1 << 2,
        Pause = 1 << 3
    }
}