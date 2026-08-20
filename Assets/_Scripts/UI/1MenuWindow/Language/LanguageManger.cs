using Assets._Scripts.GameMVP.Language;
using Assets.ScriptableObjects.Language;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.UI._1MenuWindow.Language
{
    public class LanguageManger : IDisposable
    {
        private List<ILanguageFlip> _languageFlipList;
        private List<LanguageConfig> _languages;
        private int _idLanguage;

        public LanguageManger(List<LanguageConfig> languages) 
        {
            _idLanguage = 0;
            _languages = languages;
            _languageFlipList = new List<ILanguageFlip>();
        }

        public void Initialization(GameSaveData gameSaveData)
        {
            var data = gameSaveData.SettingsData;
            _idLanguage = data.IdLanguage;
        }

        public void Dispose()
        {
            _languageFlipList.Clear();
        }

        public void SetLanguageId(int id)
        {
            _idLanguage = id;
            var language = _languages[id];

            for (int i = 0; i < _languageFlipList.Count; i++) 
            {
                _languageFlipList[i].SetLanguage(language);
            }
        }

        public void AddLangageFlip(ILanguageFlip langageFlip)
        {
            _languageFlipList.Add(langageFlip);

            langageFlip.SetLanguage(_languages[_idLanguage]);
        }

        public void RemoveLanguageFlip(ILanguageFlip langageFlip) 
        {
            _languageFlipList.Remove(langageFlip);
        }
    }
}
