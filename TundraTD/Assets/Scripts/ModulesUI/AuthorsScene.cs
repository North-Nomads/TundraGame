using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModulesUI
{
    public class AuthorsScene : MonoBehaviour
    {
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
