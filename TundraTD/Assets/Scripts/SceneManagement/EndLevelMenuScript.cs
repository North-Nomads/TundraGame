using ModulesUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using CanvasGroup = ModulesUI.CanvasGroup;

namespace SceneManagement
{
    public class EndLevelMenuScript : TundraCanvas
    {
        public override CanvasGroup CanvasGroup => CanvasGroup.Pause;
        public override CanvasGroup BlockList => CanvasGroup.Everything;
        
        private const int MainMenuSceneID = 0;

        public void KeepPlaying(string result)
        {
            UIToggle.ResetValues();
            if (result == "victory" && SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if (result == "victory")
                SceneManager.LoadScene(MainMenuSceneID);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MoveToMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneID);
        }
    }
}