using Assets.Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.GameMVP.Language
{
    public class LanguageModel
    {
        private int _id;
        private bool _isActive;

        public event Action<int> OnLanguageChanged;
        public event Action<bool> OnActivateChanged;

        public LanguageModel(int id = 0, bool isActivate = false)
        {
            _id = id;
            _isActive = isActivate;
        }

        public void Save(GameSaveData gameSaveData)
        {
            gameSaveData.SettingsData.IdLanguage = _id;
        }

        public void Load(GameSaveData gameSaveData)
        {
            var data = gameSaveData.SettingsData;
            SetIdLanguage(data.IdLanguage);
        }

        public void SetActivate(bool isAcivate)
        {
            _isActive = isAcivate;
            OnActivateChanged?.Invoke(_isActive);
        }

        public void SetIdLanguage(int id)
        {
            _id = id;
            OnLanguageChanged?.Invoke(_id);
        }
    }
}
