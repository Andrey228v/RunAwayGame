using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.UI._1MenuWindow.Language;
using Assets.Scripts.SaveLoad.Data;
using UnityEngine;

namespace Assets._Scripts.GameMVP.Language
{
    public class LanguageController : IInitGame, ISaveGame, ILoadGame
    {
        private LanguageModel _model;
        private LanguageViewMenu _viewMenu;

        public LanguageController(LanguageModel model)
        {
            _model = model;
            
        }

        public void Initialization(GameSaveData gameSaveData)
        {

        }

        public void Dispose()
        {

        }

        public void Save(GameSaveData gameSaveData)
        {

        }

        public void Load(GameSaveData gameSaveData)
        {

        }

        public void AddMenuView(LanguageViewMenu viewMenu)
        {
            _viewMenu = viewMenu;
        }

    }
}
