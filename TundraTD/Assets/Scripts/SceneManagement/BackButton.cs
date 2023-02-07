using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    /// <summary>
    /// A control over a back button in Levels scene
    /// </summary>
    public class BackButton : MonoBehaviour
    {
        private const int MainMenuSceneID = 1;

        public void ReturnBackToMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneID);
        }
    }
}