using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.GameMVP.Language
{
    public enum LanguageType
    {
        Russian = 0,
        English = 1,
        Turkish = 2,
        None = 100,
    }

    public class LanguageModel
    {
        private LanguageType _currentLanguage = LanguageType.None;
        private bool _isMenuVisible;

        public event Action OnLanguageChangedForSave;
        public event Action<LanguageType> OnLanguageChanged;
        public event Action<int> OnLangageIdChanged;
        public event Action<bool> OnMenuVisibilityChanged;

        public LanguageType CurrentLanguage => _currentLanguage;


        public void Save(GameSaveData gameSaveData)
        {
            if (gameSaveData.SettingsData == null)
            {
                gameSaveData.SettingsData = new SettingsData();
            }
            gameSaveData.SettingsData.IdLanguage = (int)_currentLanguage;
        }

        public void Load(GameSaveData gameSaveData)
        {
            var settings = gameSaveData.SettingsData;

            if (settings != null)
            {
                SetLanguage((LanguageType)settings.IdLanguage);
            }
        }

        public void ToggleMenuVisibility()
        {
            _isMenuVisible = !_isMenuVisible;
            SetVisibility(_isMenuVisible);
        }

        public void SetLanguage(LanguageType language)
        {
            _currentLanguage = language;
            OnLanguageChanged?.Invoke(language);
            _isMenuVisible = false;
            SetVisibility(_isMenuVisible);
            OnLangageIdChanged?.Invoke(((int)language));
            OnLanguageChangedForSave?.Invoke();
        }

        private void SetVisibility(bool IsVisible)
        {
            OnMenuVisibilityChanged?.Invoke(_isMenuVisible);
        }
    }
}
