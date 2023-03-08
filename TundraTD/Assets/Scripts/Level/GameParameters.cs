using System;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// The class for the
    /// </summary>
    public static class GameParameters
    {
        private static float _musicVolumeModifier = 1;
        private static float _effectsVolumeModifier = 1;

        public static float TotalVolumeModifier
        {
            get
            {
                return AudioListener.volume;
            }
            set
            {
                AudioListener.volume = value;
            }
        }

        public static float MusicVolumeModifier
        {
            get => _musicVolumeModifier;
            set
            {
                _musicVolumeModifier = Mathf.Clamp01(value);
                MusicVolumeChanged(null, null);
            }
        }

        public static float EffectsVolumeModifier
        {
            get => _effectsVolumeModifier;
            set
            {
                _effectsVolumeModifier = Mathf.Clamp01(value);
                EffectsVolumeChanged(null, null);
            }
        }

        public static event EventHandler MusicVolumeChanged = delegate { };

        public static event EventHandler EffectsVolumeChanged = delegate { };
    }
}