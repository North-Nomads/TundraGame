using Level;
using UnityEngine;

public class GameplayMusic : MonoBehaviour
{
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        if (GameParameters.MusicVolumeModifier != 0)
            _source.Play();
    }

}
