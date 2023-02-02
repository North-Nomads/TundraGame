using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void Level()
    {
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        SceneManager.LoadScene(1);
    }
}
