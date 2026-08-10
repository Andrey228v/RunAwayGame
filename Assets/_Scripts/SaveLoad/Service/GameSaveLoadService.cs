using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.SaveLoad.Service
{
    public class GameSaveLoadService : IDisposable
    {
        private readonly EasySaveSystem _saveSystem;
        private GameSaveData _gameSaveData;
        private readonly IGameLogger _gameLogger;

        public GameSaveData GameSaveData => _gameSaveData;

        public GameSaveLoadService(EasySaveSystem saveSystem,
            IGameLogger gameLogger) 
        {
            _saveSystem = saveSystem;
            _gameLogger = gameLogger;

            LoadOrCreateSave();
        }

        public void Dispose()
        {
            _gameLogger.Log("GameSaveLoadService Dispose", "Service");

            SaveGame();

            _gameLogger.Log("GameSaveLoadService Dispose complite", "Service");
        }

        public void SaveGame()
        {
            _gameLogger.Log("GameSaveLoadService Save game", "Save");
            _gameSaveData.LastSaveTime = DateTime.Now;
            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
        }

        private void LoadOrCreateSave()
        {
            _gameLogger.Log("GameSaveLoadService LoadOrCreateSave", "Service");

            if (_saveSystem.HasKey(SaveUtilites.GAME_SAVE_KEY))
            {
                _gameSaveData = _saveSystem.Load<GameSaveData>(SaveUtilites.GAME_SAVE_KEY);
            }
            else
            {
                _gameSaveData = new GameSaveData(
                    new Dictionary<string, LevelData>(),
                    new Dictionary<string, AchievmentData>(),
                    new ShopData(),
                    new WalletData(67, 322),
                    new SettingsData(),
                    DateTime.Now){ };

                _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
            }
        }
    }
}
