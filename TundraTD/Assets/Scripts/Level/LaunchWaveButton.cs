using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level
{
    public class LaunchWaveButton : MonoBehaviour
    {
        [SerializeField] private Canvas magicUI;
        [SerializeField] private LevelCornerman levelCornerman;

        private void Start()
        {
            if (magicUI == null || levelCornerman == null)
                throw new ArgumentNullException("magicUI, levelCornerman",
                    "magicUI or levelCorner man were not assigned");
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