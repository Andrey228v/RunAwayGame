using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.ObjectsScripts.Points.CheckPoint;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    //Основная цель сделать так, чтобы уровень не зависил от частей его наполнения и они могли быть разной конфигурации.
    // для этого всё делает интерфейсами, выделяя в них только общее и вынося уже для каждого в своё.
    // Разные типы уровней будут обладать разными LevelEntryPoin. Благодаря этому их можно наполнять по разному.
    public class LevelEnteryPoint : IStartable, IDisposable
    {
        private CheckPointsController _checkPointsController;
        private CoinController _coinController;
        private LevelsController _levelsController;
        private GameSaveLoadService _gameSaveLoadService;
        private WalletController _walletController;
        private LevelConfig _levelConfig;
        private FinishController _finishController;
        private GameLoopService _gameLoopController;
        private FinishModel _finishModel;
        private CoinDictinaryModel _coinDictinaryModel;
        private CheckPointDictinaryModel _checkPointDictinaryModel;
        private LastCheckPointController _lastCheckPointController;

        private readonly List<IInit> _initList; // каждый подэлемент должен сам инициализировать то, что будет. 
        private readonly List<ISave> _saveList;
        private readonly List<ILoad> _loadList;
        private readonly List<IDieRestart> _restartList;
        private readonly List<IFinish> _finishList;
        private readonly List<IReset> _resetList;

        public LevelEnteryPoint(GamePoints gamePoints,
            CheckPointsController checkPointsController, 
            CoinController coinController,
            GameSaveLoadService gameSaveLoadService,
            WalletController walletController,
            LevelConfig levelConfig,
            FinishController finishController,
            GameLoopService gameLoopController,
            FinishModel finishModel, 
            CoinDictinaryModel coinDictinaryModel,
            CheckPointDictinaryModel checkPointDictinaryModel,
            LastCheckPointController lastCheckPointController,
            LevelsController levelsController)
        {
            _checkPointsController = checkPointsController;
            _coinController = coinController;
            _levelsController = levelsController;
            _gameSaveLoadService = gameSaveLoadService;
            _levelConfig = levelConfig;
            _walletController = walletController;
            _finishController = finishController;
            _gameLoopController = gameLoopController;
            _coinDictinaryModel = coinDictinaryModel;
            _checkPointDictinaryModel = checkPointDictinaryModel;
            _finishModel = finishModel;
            _lastCheckPointController = lastCheckPointController;

            _saveList = new List<ISave>();
            _loadList = new List<ILoad>();
            _restartList = new List<IDieRestart>();
            _finishList = new List<IFinish>();
            _resetList = new List<IReset>();
        }

        public void Start()
        {
            _levelsController.SetLevelConfig(_levelConfig);
            _levelsController.Initialization(_gameSaveLoadService.GameSaveData); // Тут последовательности важна. Подумать как переделать.
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];

            _coinDictinaryModel.OnObjectAdd += CoinAdd;
            _checkPointDictinaryModel.OnObjectAdd += CheckPointAdd;

            _coinController.Initialization(levelData, _levelConfig);
            _checkPointsController.Initialization(levelData, _levelConfig);
            _lastCheckPointController.Initialization(levelData, _levelConfig);

            //_levelsController.SetLevelData(_gameSaveLoadService.GameSaveData, _levelConfig);

            _saveList.Add(_coinController);
            _loadList.Add(_coinController);
            _restartList.Add(_coinController);
            _finishList.Add(_coinController);

            _saveList.Add(_checkPointsController);
            _loadList.Add(_checkPointsController);
            _restartList.Add(_checkPointsController);
            _finishList.Add(_checkPointsController);

            _finishList.Add(_finishController);

            _saveList.Add(_lastCheckPointController);
            _loadList.Add(_lastCheckPointController);
            _restartList.Add(_lastCheckPointController);
            _finishList.Add(_lastCheckPointController);

            LoadAllServices(levelData);

            _finishModel.OnObjectStatusChange += _gameLoopController.FinishLevel;
        }

        public void Dispose()
        {
            _checkPointsController.Dispose();
            _coinController.Dispose();
            _finishController.Dispose();
            _lastCheckPointController.Dispose();

            _finishModel.OnObjectStatusChange -= _gameLoopController.FinishLevel;
            _coinDictinaryModel.OnObjectAdd -= CoinAdd;

            foreach (var model in _coinDictinaryModel.ObjectModelds.Values)
            {
                model.OnTakeValue -= _walletController.AddConis;
                //model.OnTake -= _gameSaveLoadService.SaveAllServices;
            }

            _saveList.Clear();
            _loadList.Clear();
            _restartList.Clear();
            _finishList.Clear();
            _resetList.Clear();
        }

        private void CoinAdd(CoinModel model)
        {
            model.OnTakeValue += _walletController.AddConis;
            //model.OnTake += _gameSaveLoadService.SaveAllServices;
        }

        private void CheckPointAdd(CheckPointModel model)
        {
            //model.OnTake += _gameSaveLoadService.SaveAllServices;
            model.OnTakePosition += _lastCheckPointController.SetData;
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];

            if (gameSaveData == null)
            {
                throw new ArgumentNullException(nameof(gameSaveData), "gameSaveData cannot be null");
            }

            foreach (ISave save in _saveList)
            {
                save.Save(levelData);
            }
        }

        public void LoadAllServices(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            if (_levelConfig == null)
            {
                return;
            }

            foreach (ILoad load in _loadList)
            {
                load.Load(levelData);
            }
        }

        public void DieRestart(GameSaveData gameSaveData)
        {
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];

            foreach (IDieRestart restart in _restartList)
            {
                restart.DieRestart(levelData);
            }
        }

        public void FinishLevel(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            foreach (IFinish finish in _finishList)
            {
                finish.Finish(levelData);
            }
        }

        public void ResetLevel(LevelData levelData, LevelConfig levelConfig)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            foreach (IReset reset in _resetList)
            {
                reset.ResetAllObjects(levelConfig);
            }
        }

    }
}
