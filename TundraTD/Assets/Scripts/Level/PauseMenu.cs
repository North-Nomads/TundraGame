using UnityEngine;

namespace Level
{
    public class PauseMenu : MonoBehaviour
    {
        private bool _isGamePaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                TurnOnOffPause();
        }

        private void TurnOnOffPause()
        {
            Debug.Log(!_isGamePaused);
            if (_isGamePaused)
            {
                _isGamePaused = false;
                Time.timeScale = 1;
                return;
            }
            
            Time.timeScale = 0;
            _isGamePaused = true;
        }
    }
}