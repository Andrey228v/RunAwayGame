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
        //private ISaveSystem _saveSystem;
        private PlayerData _playerData;
        private IObjectResolver _container;
        //private List<CheckPoint> _checkPoints;
        private int _checkpointsCount;
        private GamePoints _gamePoints;
        //private string _levelId;
        private ISaveService _saveService;
        //private string _levelID;
        private LevelData _startData;
        private LevelData _loadData;
        private CheckPointsController _checkPointsController;
        private GamePanelController _gamePanelController;

        public GameEnteryPoint(PlayerController playerController,
            PlayerStateMachineFactory playerStateMachineFactory, 
            CameraController cameraController,
            Func<Character> characterFactory, PlayerData playerData,
            IObjectResolver container, GamePoints gamePoints,
            ISaveService saveService, LevelData startData,
            CheckPointsController checkPointsController,
            GamePanelController gamePanelController)
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            //_saveSystem = saveSystem;
            _playerData = playerData;
            _container = container;
            _gamePoints = gamePoints;
            _startData = startData; // ситуативно пока оставить, потом придмать что-то потому что загрузка будет из общего словаря...
            _saveService = saveService;
            //_levelID = levelID; // под вопросом
            _checkPointsController = checkPointsController;
            _gamePanelController = gamePanelController;
        }

        public void Start()
        {
            _checkPointsController.OnSave += _saveService.SaveLevelData;
            _gamePanelController.OnButtonLoadClick += _saveService.LoadLevel;
            _gamePanelController.OnButtonSaveClick += _saveService.SaveLevelData;


            _saveService.SetLevelId(_startData.LevelID);
            _loadData = _saveService.GetLevelData();

            _loadData = InitPlayer(_loadData);
            _loadData = InitCheckPoints(_loadData);

            _saveService.SaveLevelIntoGame(_loadData);
            _saveService.LoadLevel();
        }

        public void Dispose()
        {
            _checkPointsController.OnSave -= _saveService.SaveLevelData;
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

                //character.transform.SetLocalPositionAndRotation(playerData.PlayerPosition, playerData.PlayerRotation);
                levelData.PlayerData = playerData;
            }
            else
            {
                //var playerData = _loadData.PlayerData;
                //_playerData.PlayerPosition = playerData.PlayerPosition;
                //_playerData.PlayerRotation = playerData.PlayerRotation;

                //character.transform.SetLocalPositionAndRotation(_playerData.PlayerPosition, _playerData.PlayerRotation);
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
                _checkpointsCount = gameCheckPointList.Count;
                
                for(int i = 0; i < loadCheckPointsData.Count; i++)
                {
                    loadCheckPointsData[i] = new CheckPointData { Id = gameCheckPointList[i].Id, IsActivated = gameCheckPointList[i].IsActivated };
                    _loadData.CheckPoints = loadCheckPointsData;
                }

                Debug.Log(_checkpointsCount);
            }
            else
            {

            }

            return levelData;
        }


    }
}
