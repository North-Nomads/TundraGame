using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class EndLevelMenuScript : MonoBehaviour
    {
        private const int LevelsSceneID = 1;

        public void KeepPlaying(string result)
        {
            if (result == "victory" && SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if (result == "victory")
                SceneManager.LoadScene(LevelsSceneID);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MoveToLevelsScene()
        {
            SceneManager.LoadScene(LevelsSceneID);
        }
    }
}