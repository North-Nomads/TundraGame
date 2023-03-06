using System;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.Pause
{
    /// <summary>
    /// A parent of all 
    /// </summary>
    public class PauseParent : MonoBehaviour
    {
        [Header("Hierarchy objects")]
        [SerializeField] private PauseCanvas pauseCanvas;
        [SerializeField] private Button hudPauseButton;
        [SerializeField] private Transform hideOnPauseCanvases;
        public PauseCanvas PauseCanvas => pauseCanvas;

        /// <summary>
        /// Is called on pause button clicked 
        /// </summary>
        public void OpenPauseUI()
        {
            PauseMode.SetPause(true);
        }

        private void HandlePauseSwitching(object sender, bool value)
        {
            pauseCanvas.gameObject.SetActive(value);
            hudPauseButton.gameObject.SetActive(!value);
            hideOnPauseCanvases.gameObject.SetActive(!value);
        }

        public void SubscribeToPauseMode()
        {
            PauseMode.PauseStateSwitched += HandlePauseSwitching;
            PauseMode.SetPause(false);
        }
    }
}