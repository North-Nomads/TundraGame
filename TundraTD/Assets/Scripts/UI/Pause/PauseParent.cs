using Level;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace UI.Pause
{
    /// <summary>
    /// A parent of all 
    /// </summary>
    public class PauseParent : MonoBehaviour
    {
        [SerializeField] private PauseCanvas pauseCanvas;
        [SerializeField] private Button hudPauseButton;

        /// <summary>
        /// Is called on pause button clicked 
        /// </summary>
        public void OpenPauseUI()
        {
            Debug.Log("OpenPause: PauseParent");
            PauseMode.SetPause(true);
        }

        private void HandlePauseSwitching(object sender, bool value)
        {
            pauseCanvas.gameObject.SetActive(value);
            hudPauseButton.gameObject.SetActive(!value);
        }

        public void SubscribeToPauseMode()
        {
            PauseMode.PauseStateSwitched += HandlePauseSwitching;
            PauseMode.SetPause(false);
        }
    }
}