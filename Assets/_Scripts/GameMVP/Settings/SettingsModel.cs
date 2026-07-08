using Assets._Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.GameControllers.Settings
{
    public class SettingsModel
    {
        private SettingsData _settingsData;
        private double _minSliderValue = 0.01;
        private double _maxSliderValue = 1;

        public event Action<double> OnAudioValueChanged;
        public event Action<double> OnMusicValueChanged;
        public event Action<double> OnEffectsValueChanged;
        public event Action<bool> OnSoundStatusChanged;

        public void SetAudioValue(double value)
        {
            if(value < _minSliderValue)
            {
                value = _minSliderValue;
            }
            else if( value > _maxSliderValue)
            {
                value = _maxSliderValue;
            }

            _settingsData.VolumeAudio = value;
            OnAudioValueChanged?.Invoke(value);
        }

        public void SetMusicValue(double value) 
        {
            if (value < _minSliderValue)
            {
                value = _minSliderValue;
            }
            else if (value > _maxSliderValue)
            {
                value = _maxSliderValue;
            }

            _settingsData.VolumeMusic = value;
            OnMusicValueChanged?.Invoke(value);
        }

        public void SetEffectsValue(double value) 
        {
            if (value < _minSliderValue)
            {
                value = _minSliderValue;
            }
            else if (value > _maxSliderValue)
            {
                value = _maxSliderValue;
            }

            _settingsData.VolumeEffects = value;
            OnEffectsValueChanged?.Invoke(value);
        }

        public void SetSoundStatus(bool value)
        {
            _settingsData.IsSoundOn = value;
            OnSoundStatusChanged?.Invoke(value);
        }
    }
}
