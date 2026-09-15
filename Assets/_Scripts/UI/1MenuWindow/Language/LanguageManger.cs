using Assets._Scripts.GameMVP.Language;
using Assets.ScriptableObjects.Language;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.UI._1MenuWindow.Language
{
    //Управление передачей выбранного языка в другие вьюшки элементам, чтобы там происходило обновление.
    //Нужно ли деление на Контроллер языка и менеджер - пока не понятно плюсов от разделения.
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

        // подписка на событиек от модели.
        public void SetLanguageId(int id)
        {
            _idLanguage = id;
            var language = _languages[id];

            for (int i = 0; i < _languageFlipList.Count; i++) 
            {
                _languageFlipList[i].SetLanguage(language);
            }
        }

        //Сюда мы добавляем элементы, в которых должен меняться язык.
        public void AddLangageFlip(ILanguageFlip langageFlip)
        {
            _languageFlipList.Add(langageFlip);

            langageFlip.SetLanguage(_languages[_idLanguage]);
        }

        //Удаляем элементы при Dispose.
        public void RemoveLanguageFlip(ILanguageFlip langageFlip) 
        {
            _languageFlipList.Remove(langageFlip);
        }
    }
}
