using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class EndLevelMenuScript : MonoBehaviour
    {
        private const int LevelsSceneID = 0;

        public void KeepPlaying(string result)
        {
            if (result == "victory" && SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if (result == "victory")
                Debug.LogError("No More Scenes to load");
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MoveToLevelsScene()
        {
            SceneManager.LoadScene(LevelsSceneID);
        }
    }
}