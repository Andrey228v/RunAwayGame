using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Service;
using Assets.Scripts.StateMachines.Player;
using Assets.Scripts.UI;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : IStartable, IDisposable
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        private int _checkpointsCount;
        private GamePoints _gamePoints;
        private SaveLoadService _saveService;
        private LevelData _startData;
        private LevelData _loadData;
        private CheckPointsController _checkPointsController;
        private IEnumerable<ISaveLoad> _saveLoads;


        public GameEnteryPoint(PlayerController playerController,
            PlayerStateMachineFactory playerStateMachineFactory, 
            CameraController cameraController,
            Func<Character> characterFactory, PlayerData playerData,
            IObjectResolver container, GamePoints gamePoints,
            SaveLoadService saveService, LevelData startData,
            CheckPointsController checkPointsController,
            IEnumerable<ISaveLoad> saveLoads)
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            _gamePoints = gamePoints;
            _startData = startData; // ситуативно пока оставить, потом придмать что-то потому что загрузка будет из общего словаря...
            _saveService = saveService;
            _checkPointsController = checkPointsController;
            _saveLoads = saveLoads;
            //_gamePanelController = gamePanelController;
        }

        public void Start()
        {
            _checkPointsController.OnSave += _saveService.SaveLevelData;
            //_gamePanelController.OnButtonLoadClick += _saveService.LoadLevel;
            //_gamePanelController.OnButtonSaveClick += _saveService.SaveLevelData;


            _saveService.SetLevelId(_startData.LevelID);
            _saveService.SetLevelObjects(_saveLoads);

            _loadData = _saveService.GetLevelData();

            _loadData = InitPlayer(_loadData);
            _loadData = InitCheckPoints(_loadData);

            _saveService.SaveLevelIntoGame(_loadData);
            _saveService.LoadLevel();
        }

        public void Dispose()
        {
            _checkPointsController.OnSave -= _saveService.SaveLevelData;
            //_gamePanelController.OnButtonLoadClick -= _saveService.LoadLevel;
            //_gamePanelController.OnButtonSaveClick -= _saveService.SaveLevelData;
        }

        private LevelData InitPlayer(LevelData levelData)
        {
            Character character = _characterFactory();

            if (levelData.PlayerData == null)
            {
                var playerData = new PlayerData
                {
                    PlayerPosition = _gamePoints.StartPoint.transform.position,
                    PlayerRotation = new Quaternion(0, 0, 0, 0)
                };

                levelData.PlayerData = playerData;
            }

            _cameraController.SetTarget(character.transform);
            PlayerStateMachine playerStateMachine = _playerStateMachineFactory.Create(character, _cameraController);
            _playerController.SetCharacter(character);
            _playerController.SetPlayerStateMachine(playerStateMachine);

            return levelData;
        }

        private LevelData InitCheckPoints(LevelData levelData) // ПОД ВОПРОСОМ....
        {
            List<CheckPointData> loadCheckPointsData = levelData.CheckPoints;
            List<CheckPoint> gameCheckPointList = _checkPointsController.TransformToList(_gamePoints.CheckPoints);

            if (loadCheckPointsData == null)
            {
                loadCheckPointsData = new List<CheckPointData>();
                _checkpointsCount = gameCheckPointList.Count;
                
                for(int i = 0; i < gameCheckPointList.Count; i++)
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
