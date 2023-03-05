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
            Debug.Log("Resume: PauseCanvas");
            PauseMode.SetPause(false);
        }

        public void RestartScene()
        {
            Debug.Log("Restart: PauseCanvas");

            PauseMode.SetPause(false);
            PauseMode.ResetSubscribers();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            Debug.Log("Return: PauseCanvas");
            PauseMode.SetPause(false);
            PauseMode.ResetSubscribers();
            SceneManager.LoadScene(0);
        }
    }
}