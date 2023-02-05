﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void KeepPlaying(string result)
    {
        if (result == "victory" && SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else if (result == "victory")
            Debug.LogError("No More Scenes to load");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    public void Levels()
    {
        SceneManager.LoadScene(0);
    }
}
