using Assets.Scripts.Spells;
using ModulesUI.MagicScreen;
using ModulesUI.Pause;
using Spells;
using UnityEngine;


namespace Level
{
    /// <summary>
    /// Handles the order of loading and initializing objects on level
    /// </summary>
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [Tooltip("An object that holds pause button and panel")]
        [SerializeField] private PauseParent pauseParent;
        [SerializeField] private MagicSpell[] spellInitializers;
        [SerializeField] private MobPool[] mobPools;
        [SerializeField] private AdditionalSpellEffect[] additionalSpellEffects;


        private void Start()
        {

            InitializePauseMode();
            InitializeArchitectValues();
            PlayerDeck.DeckElements.Clear();
            ResetMobPools();
        }

        private void ResetMobPools()
        {
            foreach (var mobPool in mobPools)
            {
                mobPool.ResetValues();
            }
        }

        private void InitializePauseMode()
        {
            PauseMode.ResetSubscribers();
            PauseMode.SetPause(false);
            pauseParent.SubscribeToPauseMode();
            pauseParent.PauseCanvas.SetImmortalAudioSource(audioSource);
        }

        private void InitializeArchitectValues()
        {
            MagicSpell.SetSpellPrefabs(spellInitializers, additionalSpellEffects);
        }
    }
}