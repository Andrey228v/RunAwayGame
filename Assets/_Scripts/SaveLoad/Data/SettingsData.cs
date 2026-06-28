using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.SaveLoad.Data
{
    [Serializable]
    public class SettingsData
    {
        public double VolumeAudio;
        public double VolumeMusic;
        public double VolumeEffects;
        public bool IsSoundOn;

        public SettingsData(double volumeAudion = 0.5, double volumeMusic = 0.5, double volumeEffects = 0.5, bool isSoundOn = true)
        {
            VolumeAudio = volumeAudion;
            VolumeMusic = volumeMusic;
            VolumeEffects = volumeEffects;
            IsSoundOn = isSoundOn;
        }

        public void ResetData()
        {
            VolumeAudio = 0.5;
            VolumeMusic = 0.5;
            VolumeEffects = 0.5;
            IsSoundOn = true;
        }
    }
}
