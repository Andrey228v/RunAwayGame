using Assets._Scripts.GameControllers;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class LevelEnteryPoint : IStartable, IDisposable
    {
        private int _checkpointsCount;
        private GamePoints _gamePoints;
        private SaveLoadService _saveLoadService;
        private LevelData _loadData;
        private CheckPointsController _checkPointsController;
        private IEnumerable<ISaveLoad> _saveLoads;
        private IEnumerable<IRestart> _restartSub;
        private IEnumerable<IFinish> _finished;
        private GameCycleController _cycleController;
        private GameManager _gameManager;

        public LevelEnteryPoint(GamePoints gamePoints,
            SaveLoadService saveLoadService,
            CheckPointsController checkPointsController,
            IEnumerable<ISaveLoad> saveLoads,
            IEnumerable<IRestart> restartSub,
            IEnumerable<IFinish> finished,
            GameCycleController gameCycleController,
            GameManager gameManager)
        {
            _gamePoints = gamePoints;
            _saveLoadService = saveLoadService;
            _checkPointsController = checkPointsController;
            _saveLoads = saveLoads;
            _restartSub = restartSub;
            _finished = finished;
            _cycleController = gameCycleController;
            _gameManager = gameManager;
        }

        public void Start()
        {
            InitEvents();
            InitSaveLoadData();
        }

        public void Dispose()
        {
            _checkPointsController.OnSave -= _gameManager.SaveGameSignal;
            _gamePoints.FinishPoint.OnFinishActivated -= _gameManager.FinishGameSignal;
            _gamePoints.FinishPoint.OnRestartActivated -= _gameManager.RestartGameSignal;

            _checkPointsController.Dispose();
        }

        private void InitEvents()
        {
            _checkPointsController.OnSave += _gameManager.SaveGameSignal;
            _gamePoints.FinishPoint.OnFinishActivated += _gameManager.FinishGameSignal;
            _gamePoints.FinishPoint.OnRestartActivated += _gameManager.RestartGameSignal;
        }

        private void InitSaveLoadData()
        {
            _loadData = _saveLoadService.GetLevelData();
            _saveLoadService.AddSaveLoadSub(_checkPointsController);
            _loadData = InitCheckPoints(_loadData);

            foreach (var data in _saveLoads)
            {
                data.Load(_loadData);
            }
        }


        private LevelData InitCheckPoints(LevelData levelData) // ПОД ВОПРОСОМ....
        {
            List<CheckPointData> loadCheckPointsData = levelData.CheckPoints;
            List<CheckPoint> gameCheckPointList = _checkPointsController.TransformToList(_gamePoints.CheckPoints);

            if (loadCheckPointsData == null)
            {
                loadCheckPointsData = new List<CheckPointData>();
                _checkpointsCount = gameCheckPointList.Count;

                for (int i = 0; i < gameCheckPointList.Count; i++)
                {
                    loadCheckPointsData.Add(new CheckPointData { Id = gameCheckPointList[i].Id, IsActivated = gameCheckPointList[i].IsActivated });
                }

                _loadData.CheckPoints = loadCheckPointsData;
                Debug.Log(_checkpointsCount);
            }

            return levelData;
        }
    }
}
