using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.UI._1MenuWindow.Language;
using Assets.Scripts.SaveLoad.Data;
using System.Collections.Generic;

namespace Assets._Scripts.GameMVP.Language
{
    public class LanguageController : IInitGame, ISaveGame, ILoadGame
    {
        private LanguageModel _model;
        private Dictionary<string, ILanguageViewMenu> _dictionaryView;

        //private LanguageViewMenu _viewMenu;



        public LanguageController(LanguageModel model)
        {
            _dictionaryView = new Dictionary<string, ILanguageViewMenu>();
            _model = model;
        }

        public void Initialization(GameSaveData gameSaveData)
        {
            var data = gameSaveData.SettingsData;

            if (data == null)
            {
                data = new SettingsData();
                gameSaveData.SettingsData = data;
            }
        }

        public void Dispose()
        {

        }

        public void Save(GameSaveData gameSaveData)
        {
            _model.Save(gameSaveData);
        }

        public void Load(GameSaveData gameSaveData)
        {
            _model.Load(gameSaveData);
        }

        public void AddMenuView(string key, LanguageViewMenu viewMenu)
        {
            _dictionaryView.Add(key, viewMenu);
            _model.OnLanguageChanged += viewMenu.UpdateMainButton;
        }

        public void RemoveMenuView(string key)
        {
            _dictionaryView.Remove(key);
        }

    }
}
