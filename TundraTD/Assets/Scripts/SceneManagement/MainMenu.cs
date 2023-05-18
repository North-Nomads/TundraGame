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
        private const int FirstSceneID = 2;
        private AudioSource _source;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            GameParameters.MusicVolumeChanged += SetVolume;
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

        public void OpenTutorialScene()
        {
            SceneManager.LoadScene(2);
        }
        
        public void SwitchEffectsSound(GameObject sender)
        {
            if (GameParameters.EffectsVolumeModifier == 1)
            {
                GameParameters.EffectsVolumeModifier = 0;
                sender.GetComponentInChildren<Text>().text = "SFX\nOFF";
            }
            else
            {
                GameParameters.EffectsVolumeModifier = 1;
                sender.GetComponentInChildren<Text>().text = "SFX\nON";
            }

        }

        public void SwitchMusicSound(GameObject sender)
        {
            if (GameParameters.MusicVolumeModifier == 1)
            {
                GameParameters.MusicVolumeModifier = 0;
                sender.GetComponentInChildren<Text>().text = "Music\nOFF";
            }
            else
            {
                GameParameters.MusicVolumeModifier = 1;
                sender.GetComponentInChildren<Text>().text = "Music\nON";
            }
        }
    }
}