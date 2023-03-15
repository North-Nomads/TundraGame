using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using ModulesUI.MagicScreen;
using ModulesUI.PlayerHUD;

namespace ModulesUI
{
    public static class UIToggle
    {
        private static CanvasGroup _blockedGroups;
        public static readonly List<TundraCanvas> AllCanvases;
        public static CanvasGroup BlockedGroups => _blockedGroups;

        static UIToggle()
        {
            AllCanvases = new List<TundraCanvas>();
        }

        public static void TryOpenCanvas(TundraCanvas canvas)
        {
            if (_blockedGroups.HasFlag(canvas.CanvasGroup))
                return;
            
            _blockedGroups |= canvas.BlockList;

            foreach (var tundraCanvas in AllCanvases)
                if (tundraCanvas.isActiveAndEnabled)
                    if (canvas.BlockList.HasFlag(tundraCanvas.CanvasGroup))
                        tundraCanvas.ExecuteOnClosing();
            
            canvas.ExecuteOnOpening();
        }

        public static void HandleCanvasClosing(TundraCanvas canvas)
        {
            // Clear all groups 
            _blockedGroups = CanvasGroup.None;
            canvas.ExecuteOnClosing();
            
            // And add OpenCanvases groups again. Now without the removed one
            foreach (var tundraCanvas in AllCanvases.Where(tundraCanvas => tundraCanvas.isActiveAndEnabled))
                _blockedGroups |= tundraCanvas.CanvasGroup;
            
            if (LevelCornerman.IsInPlayMode)
                TryOpenCanvas(AllCanvases.OfType<SpellCaster>().FirstOrDefault());
            else
                TryOpenCanvas(AllCanvases.OfType<LaunchWaveButton>().FirstOrDefault());
            TryOpenCanvas(AllCanvases.OfType<CityGatesUI>().FirstOrDefault());

            // If there is anything blocking UI -> the screen is yet occupied
        }

        public static void ResetValues()
        {
            _blockedGroups = CanvasGroup.None;
            AllCanvases.Clear();
        }
        
    }
    
    [Flags]
    public enum CanvasGroup
    {
        None = 0,
        MagicHUD = 1,
        Building = 1 << 1,
        Portal = 1 << 2,
        Pause = 1 << 3,
        City = 1 << 4,
        Camera = 1 << 5,
        Everything = MagicHUD | Building | Portal | Pause | City | Camera
    }
}