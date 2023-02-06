using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class EndLevelMenuScript : MonoBehaviour
    {
        public void KeepPlaying(string result)
        {
            if (result == "victory" && SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if (result == "victory")
                Debug.LogError("No More Scenes to load");
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Levels()
        {
            SceneManager.LoadScene(0);
        }
    }
}
