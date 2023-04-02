using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModulesUI.Pause
{
    /// <summary>
    /// Responsible for UI panels  
    /// </summary>
    public class PauseCanvas : TundraCanvas
    {
        public override CanvasGroup CanvasGroup => CanvasGroup.Pause;

        public override CanvasGroup BlockList => CanvasGroup.Everything;
        
        [Header("Audio clips")] 
        [SerializeField] private AudioClip buttonClick;
        
        private AudioSource _immortalAudioSource;

        private void Start()
        {
            UIToggle.AllCanvases.Add(this);
        }

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            _immortalAudioSource.PlayOneShot(buttonClick);
            PauseMode.SetPause(false);
            SceneManager.LoadScene(0);
        }
    }
}