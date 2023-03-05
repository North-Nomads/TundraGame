using UnityEngine;

public class ImmortalAudioSource : MonoBehaviour
{
    private void Awake()
    {   
        DontDestroyOnLoad(gameObject);
    }
}
