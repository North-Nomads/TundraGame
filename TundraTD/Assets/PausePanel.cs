using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void ToMainMenu()
    {
        // TODO: uncomment it and make scene changing when the main menu scene is ready
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
