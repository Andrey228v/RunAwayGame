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
        private bool _suppressSaveNotification;

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

            if (_settingsData == null) return;

            _suppressSaveNotification = true;

            try
            {
                SetAudioValue(_settingsData.VolumeAudio);
                SetMusicValue(_settingsData.VolumeMusic);
                SetEffectsValue(_settingsData.VolumeEffects);
            }
            finally
            {
                _suppressSaveNotification = false;
            }

        }

        public void SetAudioValue(double value)
        {
            value = Clamp(value);

            if (Math.Abs(_settingsData.VolumeAudio - value) < double.Epsilon) return;

            _settingsData.VolumeAudio = value;
            OnAudioValueChanged?.Invoke(value);
            RaiseChangedForSave();

            //if (value < _minSliderValue)
            //{
            //    value = _minSliderValue;
            //}
            //else if( value > _maxSliderValue)
            //{
            //    value = _maxSliderValue;
            //}

            //_settingsData.VolumeAudio = value;
            //OnAudioValueChanged?.Invoke(value);
            //OnChangedForSave?.Invoke(); // тут проблема что при загрузке ещё раз сохраняет и получается много сохранений.
        }

        public void SetMusicValue(double value) 
        {
            value = Clamp(value);

            if (Math.Abs(_settingsData.VolumeMusic - value) < double.Epsilon) return;

            _settingsData.VolumeMusic = value;
            OnMusicValueChanged?.Invoke(value);
            RaiseChangedForSave();

            //if (value < _minSliderValue)
            //{
            //    value = _minSliderValue;
            //}
            //else if (value > _maxSliderValue)
            //{
            //    value = _maxSliderValue;
            //}

            //_settingsData.VolumeMusic = value;
            //OnMusicValueChanged?.Invoke(value);
            //OnChangedForSave?.Invoke(); // тут проблема что при загрузке ещё раз сохраняет и получается много сохранений.
        }

        public void SetEffectsValue(double value) 
        {
            value = Clamp(value);

            if (Math.Abs(_settingsData.VolumeEffects - value) < double.Epsilon) return;

            _settingsData.VolumeEffects = value;
            OnEffectsValueChanged?.Invoke(value);
            RaiseChangedForSave();


            //if (value < _minSliderValue)
            //{
            //    value = _minSliderValue;
            //}
            //else if (value > _maxSliderValue)
            //{
            //    value = _maxSliderValue;
            //}

            //_settingsData.VolumeEffects = value;
            //OnEffectsValueChanged?.Invoke(value);
            //OnChangedForSave?.Invoke(); // тут проблема что при загрузке ещё раз сохраняет и получается много сохранений.
        }

        public void SetSoundStatus(bool value)
        {
            _settingsData.IsSoundOn = value;
            OnSoundStatusChanged?.Invoke(value);

            RaiseChangedForSave();

            //OnChangedForSave?.Invoke(); // тут проблема что при загрузке ещё раз сохраняет и получается много сохранений.
        }

        public void Reset()
        {
            _suppressSaveNotification = true;

            try
            {
                SetAudioValue(0.5);
                SetMusicValue(0.5);
                SetEffectsValue(0.5);
                SetSoundStatus(true);
            }
            finally 
            { 
                _suppressSaveNotification = false; 
            }

            RaiseChangedForSave(); // одно сохранение на весь reset

        }

        private void RaiseChangedForSave()
        {
            if (_suppressSaveNotification) return;
            OnChangedForSave?.Invoke();
        }

        private double Clamp(double value)
        {
            if (value < _minSliderValue) return _minSliderValue;
            if (value > _maxSliderValue) return _maxSliderValue;
            return value;
        }
    }
}
