using System;
using UnityEngine;

namespace Level
{
    public class LaunchWaveButton : MonoBehaviour
    {
        [SerializeField] private Canvas magicUI;
        [SerializeField] private LevelCornerman levelCornerman;

        private void Start()
        {
            if (magicUI is null || levelCornerman is null)
                throw new Exception("magicUI or levelCorner man were not assigned");
            magicUI.gameObject.SetActive(false);
        }

        public void LaunchFirstWave()
        {
            magicUI.gameObject.SetActive(true);
            levelCornerman.StartFirstWave();
            gameObject.SetActive(false);
        } 
    }
}