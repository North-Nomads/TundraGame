using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Spells;
using City;
using City.Building;
using ModulesUI;
using ModulesUI.MagicScreen;
using ModulesUI.Pause;
using ModulesUI.PlayerHUD;
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
        [SerializeField] private int minWaveAward;
        [SerializeField] private int maxWaveAward;
        [SerializeField] private Transform canvasesParent;
        [SerializeField] private CityGatesUI influencePointsHolder;
        [SerializeField] private MagicSpell[] spellInitializers;
        [SerializeField] private MobPool[] mobPools;
        
        
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
            Architect.InfluencePointsHolder = influencePointsHolder;
            Architect.CanvasesHierarchyParent = canvasesParent;
            Architect.WaveCompletionMinInfluencePointsAward = minWaveAward;
            Architect.WaveCompletionMaxInfluencePointsAward = maxWaveAward;
            MagicSpell.SetPrefabs(spellInitializers);
            //// TODO: print here the path to load additional effects.
            //MagicSpell.AdditionalSpellEffects = Resources.LoadAll<AdditionalSpellEffect>("path/to/load").ToDictionary(x => x.Element, y => y);

            // DEBUG: Temporary giving 100 points
            Architect.DEBUG_GetStartPoints();
        }
    }
}