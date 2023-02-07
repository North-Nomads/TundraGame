using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    /// <summary>
    /// Main Menu buttons events handling
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        private const int PolygonSceneID = 2;
        private const int LevelsSceneID = 0;

        public void MoveToPolygonScene()
        {
            SceneManager.LoadScene(PolygonSceneID);
        }

        public void MoveToLevelsScene()
        {
            SceneManager.LoadScene(LevelsSceneID);
        }
    }
}