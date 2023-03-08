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
        private const int PolygonSceneID = 2;
        private const int LevelsSceneID = 1;
        private AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
            GameParameters.MusicVolumeChanged += SetVolume;
        }

        private void OnDestroy()
        {
            GameParameters.MusicVolumeChanged -= SetVolume;
        }

        private void SetVolume(object sender, EventArgs e)
        {
            source.volume = GameParameters.MusicVolumeModifier;
        }

        public void MoveToPolygonScene()
        {
            SceneManager.LoadScene(PolygonSceneID);
        }

        public void MoveToLevelsScene()
        {
            SceneManager.LoadScene(LevelsSceneID);
        }

        public void SwitchEffectsSound(GameObject sender)
        {
            if (GameParameters.EffectsVolumeModifier == 1)
            {
                GameParameters.EffectsVolumeModifier = 0;
                sender.GetComponentInChildren<Text>().text = "SFX\n¬€ À";
            }
            else
            {
                GameParameters.EffectsVolumeModifier = 1;
                sender.GetComponentInChildren<Text>().text = "SFX\n¬ À";
            }

        }

        public void SwitchMusicSound(GameObject sender)
        {
            if (GameParameters.MusicVolumeModifier == 1)
            {
                GameParameters.MusicVolumeModifier = 0;
                sender.GetComponentInChildren<Text>().text = "ÃÛÁ˚Í‡\n¬€ À";
            }
            else
            {
                GameParameters.MusicVolumeModifier = 1;
                sender.GetComponentInChildren<Text>().text = "ÃÛÁ˚Í‡\n¬ À";
            }
        }
    }
}