using System;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    /// <summary>
    /// A static class that manages pause state
    /// </summary>
    public static class PauseMode
    {
        private static bool _isGamePaused;

        /// <summary>
        /// Indicates if the game is paused.
        /// </summary>
        public static bool IsGamePaused
        {
            get => _isGamePaused;
            private set
            {
                _isGamePaused = value;
                PauseStateSwitched(null, value);
            }
        }

        /// <summary>
        /// Triggers when pause state changes.
        /// </summary>
        public static event EventHandler<bool> PauseStateSwitched = delegate { };

        public static void SetPause(bool setPause)
        {
            IsGamePaused = setPause;
            Time.timeScale = setPause ? 0 : 1;
        }

        public static void ResetSubscribers()
        {
            PauseStateSwitched = delegate {  };
        }
    }
}
