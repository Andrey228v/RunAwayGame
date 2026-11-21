using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Data;
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

        public event Action OnChangedForSave;

        public void Save(GameSaveData gameSaveData)
        {
            if (gameSaveData.SettingsData == null)
            {
                gameSaveData.SettingsData = new SettingsData();
            }
        }

        public void Load(GameSaveData gameSaveData)
        {
            _settingsData = gameSaveData.SettingsData;

            if (_settingsData != null)
            {
                SetAudioValue(_settingsData.VolumeAudio);
                SetMusicValue(_settingsData.VolumeMusic);
                SetEffectsValue(_settingsData.VolumeEffects);
            }
        }

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
            OnChangedForSave?.Invoke();
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
            OnChangedForSave?.Invoke();
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
            OnChangedForSave?.Invoke();
        }

        public void SetSoundStatus(bool value)
        {
            _settingsData.IsSoundOn = value;
            OnSoundStatusChanged?.Invoke(value);
            OnChangedForSave?.Invoke();
        }

        public void Reset()
        {
            SetAudioValue(0.5);
            SetMusicValue(0.5);
            SetEffectsValue(0.5);
            SetSoundStatus(false);

        }
    }
}
