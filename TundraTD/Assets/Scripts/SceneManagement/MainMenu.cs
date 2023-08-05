using Level;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    /// <summary>
    /// Main Menu buttons events handling
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        private const int FirstSceneID = 1;
        [SerializeField] private Image audioButton;
        [SerializeField] private Sprite audioButtonOn;
        [SerializeField] private Sprite audioButtonOff;
        private AudioSource _source;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            GameParameters.MusicVolumeChanged += SetVolume;
            
            if (GameParameters.MusicVolumeModifier != 0)
                audioButton.sprite = audioButtonOn;
            else
                audioButton.sprite = audioButtonOff;
            
        }

        private void OnDestroy()
        {
            GameParameters.MusicVolumeChanged -= SetVolume;
        }

        private void SetVolume(object sender, EventArgs e)
        {
            _source.volume = GameParameters.MusicVolumeModifier;
        }

        public void MoveToPolygonScene()
        {
            SceneManager.LoadScene(FirstSceneID);
        }

        public void LoadAuthorsScene()
        {
            SceneManager.LoadScene(2);
        }

        public void SwitchSounds()
        {
            if (GameParameters.EffectsVolumeModifier == 1)
            {
                GameParameters.EffectsVolumeModifier = 0;
                GameParameters.MusicVolumeModifier = 0;
                audioButton.sprite = audioButtonOff;
            }
            else
            {
                GameParameters.EffectsVolumeModifier = 1;
                GameParameters.MusicVolumeModifier = 1;
                audioButton.sprite = audioButtonOn;
            }
        }
    }
}