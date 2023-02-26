using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Level
{
    public class PauseMode : MonoBehaviour
    {
        private static bool _isGamePaused;

        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private GameObject pausePanel;

        [SerializeField]
        private AudioSource pauseSwitchSound;

        [SerializeField]
        private Transform pauseHideObjects;

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

        private void Start()
        {
            if (pauseSwitchSound is null)
                throw new Exception("Pause switching sounds is not attached");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SwitchPause();
            }
        }

        /// <summary>
        /// Switches current pause game state.
        /// </summary>
        public void SwitchPause()
        {
            IsGamePaused = !IsGamePaused;
            Time.timeScale = IsGamePaused ? 0 : 1;
            // pauseSwitchSound.Play();
            pauseButton.gameObject.SetActive(!IsGamePaused);
            pauseHideObjects.gameObject.SetActive(!IsGamePaused);
            pausePanel.gameObject.SetActive(IsGamePaused);
        }

        public void PlayPanelSound()
        {
            pauseSwitchSound.Play();
        }

        public void SetPause(bool setPause, bool enableCanvas = true)
        {
            _isGamePaused = setPause;
            Time.timeScale = setPause ? 0 : 1;
            // pauseSwitchSound.Play();
            pauseButton.gameObject.SetActive(!IsGamePaused);
            pausePanel.gameObject.SetActive(false);
        }
    }
}
