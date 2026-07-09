using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers
{
    public class GameLoopService : IDisposable
    {
        //private LevelsController _levelsController;
        //private AchievmentsController _achievmentsController;
        //private ShopController _shopController;
        //private WalletController _walletController;
        private IGameLogger _gameLogger;
        private GameSaveLoadService _gameSaveLoadService;
        //private GameSaveData _gameSaveData;

        private readonly Dictionary<string, IInitGame> _initDict; // каждый подэлемент должен сам инициализировать то, что будет. 
        private readonly Dictionary<string, ISaveGame> _saveDict;
        private readonly Dictionary<string, ILoadGame> _loadDict;
        private readonly Dictionary<string, IDieRestartGame> _dieRestartDict;
        private readonly Dictionary<string, IFinishGame> _finishDict;
        private readonly Dictionary<string, IResetGame> _resetDict;

        public Dictionary<string, IInitGame> InitDict => _initDict;
        public Dictionary<string, ISaveGame> SaveDict => _saveDict;
        public Dictionary<string, ILoadGame> LoadDict => _loadDict;
        public Dictionary<string, IDieRestartGame> DieRestartDict => _dieRestartDict;
        public Dictionary<string, IFinishGame> FinishDict => _finishDict;
        public Dictionary<string, IResetGame> ResetDict => _resetDict;


        //LevelsController levelsController,
        //    AchievmentsController achievmentsController,
        //    ShopController shopController,
        //    WalletController walletController,
        public GameLoopService(GameSaveLoadService gameSaveLoadService,
            IGameLogger gameLogger)
        {
            //_levelsController = levelsController;
            //_achievmentsController = achievmentsController;
            //_shopController = shopController;
            //_walletController = walletController;
            _gameLogger = gameLogger;
            _gameSaveLoadService = gameSaveLoadService;
            //_gameSaveData = gameSaveLoadService.GameSaveData;

            _initDict = new Dictionary<string, IInitGame>();
            _saveDict = new Dictionary<string, ISaveGame>();
            _loadDict = new Dictionary<string, ILoadGame>();
            _dieRestartDict = new Dictionary<string, IDieRestartGame>();
            _finishDict = new Dictionary<string, IFinishGame>();
            _resetDict = new Dictionary<string, IResetGame>();
        }

        public void Dispose()
        {
            //_achievmentsController.Dispose();
            //_shopController.Dispose();
            //_walletController.Dispose();

            _initDict.Clear();
            _saveDict.Clear();
            _loadDict.Clear();
            _dieRestartDict.Clear();
            _finishDict.Clear();
            _resetDict.Clear();
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            foreach (var key in _saveDict.Keys) 
            {
                _saveDict[key].Save(gameSaveData);
            }

            _gameSaveLoadService.SaveGame();
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            foreach (var key in _loadDict.Keys)
            {
                _loadDict[key].Load(gameSaveData);
            }
        }

        public void DieRestart(GameSaveData gameSaveData)
        {
            foreach (var key in _dieRestartDict.Keys)
            {
                _dieRestartDict[key].DieRestart(gameSaveData);
            }
        }

        public void FinishLevel(GameSaveData gameSaveData)
        {
            foreach (var key in _finishDict.Keys)
            {
                _finishDict[key].Finish(gameSaveData);
            }
        }

        public void ResetLevel(GameSaveData gameSaveData)
        {
            foreach (var key in _finishDict.Keys)
            {
                _resetDict[key].Reset(gameSaveData);
            }
        }
    }
}
