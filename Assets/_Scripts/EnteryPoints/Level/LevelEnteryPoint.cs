using Assets._Scripts.EnteryPoints.Interfaces;
using Assets._Scripts.GameControllers;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class LevelEnteryPoint : IStartable, IDisposable, IInitSaveLoad, IInitFinish, IInitRestart, IInitLevel
    {
        private int _checkpointsCount;
        private GamePoints _gamePoints;
        private SaveLoadService _saveLoadService;
        private LevelData _loadData;
        private CheckPointsController _checkPointsController;
        private CoinController _coinController;
        private GameManager _gameManager;
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;

        public IEnumerable<ISaveLoad> SaveLoads { get; private set; }

        public IEnumerable<IFinish> Finished { get; private set; }

        public IEnumerable<IRestart> Restarted { get; private set; }

        public IEnumerable<IInit> Inited { get; private set; } // Надо ли ?....

        public LevelEnteryPoint(GamePoints gamePoints,
            SaveLoadService saveLoadService,
            CheckPointsController checkPointsController,
            IEnumerable<ISaveLoad> saveLoads,
            IEnumerable<IRestart> restartSub,
            IEnumerable<IFinish> finished,
            GameFinishController gameFinishController,
            GameManager gameManager, GameRestartController gameRestartController,
            CoinController coinController)
        {
            _gamePoints = gamePoints;
            _saveLoadService = saveLoadService;
            _checkPointsController = checkPointsController;
            _finishController = gameFinishController;
            _gameManager = gameManager;
            _gameRestartController = gameRestartController;
            SaveLoads = saveLoads;
            Finished = finished;
            Restarted = restartSub;
            _coinController = coinController;
        }

        public void Start()
        {
            InitEvents();
            InitLevelData();
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

        public void InitLevelData()
        {
            //_gameManager.InitGameSignal();

        }

        public void InitSaveLoadData()
        {
            _loadData = _saveLoadService.GetLevelData();
            _saveLoadService.AddSaveLoadSub(_checkPointsController);
            _saveLoadService.AddSaveLoadSub(_coinController);
            _loadData = InitCheckPoints(_loadData); // убрать ?....
            _loadData.IsLevelWasStarted = true;
            _saveLoadService.LoadPartLevelObject(SaveLoads);
        }

        public void InitFinishData()
        {
            _finishController.AddFinishSub(Finished);
        }

        public void InitRestartData()
        {
            _gameRestartController.AddRestartSub(Restarted);
        }


        private LevelData InitCheckPoints(LevelData levelData) // ПОД ВОПРОСОМ....
        {

            //_checkPointsController.SetCheckPointsParent(_gamePoints.CheckPoints);


            //List<CheckPointData> loadCheckPointsData = levelData.CheckPoints;
            //List<CheckPoint> gameCheckPointList = _checkPointsController.TransformToList(_gamePoints.CheckPoints);

            //if (loadCheckPointsData == null)
            //{
            //    loadCheckPointsData = new List<CheckPointData>();
            //    _checkpointsCount = gameCheckPointList.Count;

            //    for (int i = 0; i < gameCheckPointList.Count; i++)
            //    {
            //        loadCheckPointsData.Add(new CheckPointData { Id = gameCheckPointList[i].Id, IsActivated = gameCheckPointList[i].IsActivated });
            //    }

            //    _loadData.CheckPoints = loadCheckPointsData;
            //    Debug.Log(_checkpointsCount);
            //}

            return levelData;
        }


    }
}
