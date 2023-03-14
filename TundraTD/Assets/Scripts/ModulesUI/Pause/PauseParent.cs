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
            if (value)
                UIToggle.TryOpenCanvas(pauseCanvas);
            else
                UIToggle.HandleCanvasClosing(pauseCanvas);
            hudPauseButton.gameObject.SetActive(!value);
        }

        public void SubscribeToPauseMode()
        {
            PauseMode.PauseStateSwitched += HandlePauseSwitching;
        }
    }
}