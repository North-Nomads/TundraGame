using UnityEngine;

namespace Level
{
    public class ImmortalAudioSource : MonoBehaviour
    {
        private void Awake()
        {   
            DontDestroyOnLoad(gameObject);
        }
    }
}
