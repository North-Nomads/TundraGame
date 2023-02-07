using System;
using UnityEngine;

namespace Level
{
    public class PauseMode : MonoBehaviour
    {
        private static bool _isGamePaused;
        [SerializeField] private Canvas pauseMenu;

        /// <summary>
        /// Indicates if the game is paused.
        /// </summary>
        public static bool IsGamePaused
        {
            get => _isGamePaused;
            private set
            {
                _isGamePaused = value;
                if (value)
                {
                    PauseStateSwitched(null, null);
                }
            }
        }

        /// <summary>
        /// Triggers when pause state changes.
        /// </summary>
        public static event EventHandler PauseStateSwitched = delegate { };

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                SwitchPause();
        }

        /// <summary>
        /// Switches current pause game state.
        /// </summary>
        public void SwitchPause()
        {
            IsGamePaused = !IsGamePaused;
            Time.timeScale = IsGamePaused ? 0 : 1;
            pauseMenu.gameObject.SetActive(IsGamePaused);
            Debug.Log(IsGamePaused);
        }

        public void SetPause(bool setPause, bool enableCanvas = true)
        {
            _isGamePaused = setPause;
            Time.timeScale = setPause ? 0 : 1;
            pauseMenu.gameObject.SetActive(IsGamePaused && enableCanvas);
            Debug.Log(IsGamePaused);
        }

        public void ToMainMenu()
        {
            // TODO: uncomment it and make scene changing when the main menu scene is ready
            //SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }
}