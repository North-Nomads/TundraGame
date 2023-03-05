using System;
using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModulesUI.Pause
{
    /// <summary>
    /// Responsible for UI panels  
    /// </summary>
    public class PauseCanvas : MonoBehaviour
    {
        [Header("Audio clips")] 
        [SerializeField] private AudioClip buttonClick;
        
        private AudioSource _immortalAudioSource;

        public void SetImmortalAudioSource(AudioSource source)
        {
            _immortalAudioSource = source;
        }
        
        public void ResumeOnClick()
        {
            _immortalAudioSource.PlayOneShot(buttonClick);
            PauseMode.SetPause(false);
        }

        public void RestartScene()
        {
            _immortalAudioSource.PlayOneShot(buttonClick);
            PauseMode.SetPause(false);
            //PauseMode.ResetSubscribers();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            _immortalAudioSource.PlayOneShot(buttonClick);
            PauseMode.SetPause(false);
            //PauseMode.ResetSubscribers();
            SceneManager.LoadScene(0);
        }
    }
}