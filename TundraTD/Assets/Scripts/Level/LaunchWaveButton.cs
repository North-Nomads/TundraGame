using System;
using ModulesUI;
using UnityEngine;
using CanvasGroup = ModulesUI.CanvasGroup;

namespace Level
{
    public class LaunchWaveButton : TundraCanvas
    {
        
        public override CanvasGroup CanvasGroup => CanvasGroup.MagicHUD;
        public override CanvasGroup BlockList => CanvasGroup.None;
        
        [SerializeField] private TundraCanvas magicUI;
        [SerializeField] private LevelCornerman levelCornerman;

        private void Start()
        {
            if (magicUI == null || levelCornerman == null)
                throw new ArgumentNullException("magicUI, levelCornerman",
                    "magicUI or levelCorner man were not assigned");
            
            UIToggle.AllCanvases.Add(this);
            UIToggle.TryOpenCanvas(this);
        }

        public void LaunchFirstWave()
        {
            UIToggle.TryOpenCanvas(magicUI);
            levelCornerman.StartFirstWave();
            gameObject.SetActive(false);
        }
    }
}