using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Pause
{
    /// <summary>
    /// Responsible for UI panels  
    /// </summary>
    public class PauseCanvas : MonoBehaviour
    {
        public void ResumeOnClick()
        {
            PauseMode.SetPause(false);
        }

        public void RestartScene()
        {
            PauseMode.SetPause(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ReturnToMainMenu()
        {
            PauseMode.SetPause(false);
            SceneManager.LoadScene(0);
        }
    }
}