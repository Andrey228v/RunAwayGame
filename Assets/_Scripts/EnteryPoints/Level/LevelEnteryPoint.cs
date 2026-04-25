using Assets._Scripts.EnteryPoints.Interfaces;
using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class LevelEnteryPoint : IStartable, IDisposable, IInitFinish, IInitRestart  //IInitSaveLoad
    {
        private GamePoints _gamePoints;
        //private SaveLoadService _saveLoadService;
        //private LevelData _loadData;
        private CheckPointsController _checkPointsController;
        private CoinController _coinController;
        private GameManager _gameManager;
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;
        private GameSaveLoadService _gameSaveLoadService;

        //private LevelConfig _levelConfig;
        //private LevelsController _levelController;
        //public IEnumerable<ISaveLoad> SaveLoads { get; private set; }

        public IEnumerable<IFinish> Finished { get; private set; }

        public IEnumerable<IRestart> Restarted { get; private set; }

        public IEnumerable<IInit> Inited { get; private set; } // Надо ли ?....

        public LevelEnteryPoint(GamePoints gamePoints,
            CheckPointsController checkPointsController,
            //IEnumerable<ISaveLoad> saveLoads,
            IEnumerable<IRestart> restartSub,
            IEnumerable<IFinish> finished,
            GameFinishController gameFinishController,
            GameManager gameManager, 
            GameRestartController gameRestartController,
            CoinController coinController,
            GameSaveLoadService gameSaveLoadService)
        {
            _gamePoints = gamePoints;
            //_saveLoadService = saveLoadService;
            _checkPointsController = checkPointsController;
            _finishController = gameFinishController;
            _gameManager = gameManager;
            _gameRestartController = gameRestartController;
            //SaveLoads = saveLoads;
            Finished = finished;
            Restarted = restartSub;
            _coinController = coinController;
            //_levelController = levelController;
            //_levelConfig = saveLoadService.LevelConfig;
            _gameSaveLoadService = gameSaveLoadService;
        }

        public void Start()
        {
            var saveLoadServise = _gameSaveLoadService.GetService<LevelsController>();
            saveLoadServise.AddSerice(_coinController);
            saveLoadServise.AddSerice(_checkPointsController);

            InitEvents();
            InitSaveLoadData();
            InitFinishData();
            InitRestartData();
        }

        public void Dispose()
        {
            _checkPointsController.OnSave -= _gameManager.SaveGameSignal;
            _gamePoints.FinishPoint.OnFinishActivated -= _gameManager.FinishGameSignal;
            _gamePoints.FinishPoint.OnRestartActivated -= _gameManager.RestartGameSignal;

            _checkPointsController.Dispose();
            _coinController.Dispose();
        }

        private void InitEvents()
        {
            _checkPointsController.OnSave += _gameManager.SaveGameSignal;
            _gamePoints.FinishPoint.OnFinishActivated += _gameManager.FinishGameSignal;
            _gamePoints.FinishPoint.OnRestartActivated += _gameManager.RestartGameSignal;
        }

        public void InitSaveLoadData()
        {

            //_loadData = _saveLoadService.GetLevelData(levelConfig);
            //_saveLoadService.AddSaveLoadSub(_checkPointsController);
            //_saveLoadService.AddSaveLoadSub(_coinController);
            //_loadData.IsLevelWasStarted = true;
            //_saveLoadService.LoadPartLevelObject(SaveLoads, levelConfig);
        }

        public void InitFinishData()
        {
            _finishController.AddFinishSub(Finished);
        }

        public void InitRestartData()
        {
            _gameRestartController.AddRestartSub(Restarted);
        }
    }
}
