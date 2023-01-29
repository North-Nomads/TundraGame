using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class PauseMenu : MonoBehaviour
    {
        private static bool isGamePaused;
        [SerializeField] private GameObject pauseMenu;

        /// <summary>
        /// Indicates if the game is paused.
        /// </summary>
        public static bool IsGamePaused
        {
            get => isGamePaused;
            private set
            {
                isGamePaused = value;
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
            pauseMenu.SetActive(IsGamePaused);
            Debug.Log(IsGamePaused);
        }

        public void ToMainMenu()
        {
            // TODO: uncomment it and make scene changing when the main menu scene is ready
            //SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }
}