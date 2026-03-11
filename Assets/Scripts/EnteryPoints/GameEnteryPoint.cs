using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Service;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : IStartable
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

        public GameEnteryPoint(PlayerController playerController,
            PlayerStateMachineFactory playerStateMachineFactory, 
            CameraController cameraController,
            Func<Character> characterFactory, PlayerData playerData,
            IObjectResolver container, GamePoints gamePoints,
            ISaveService saveService, LevelData startData,
            CheckPointsController checkPointsController)
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
        }

        public void Start()
        {
            _checkPointsController.OnSave += _saveService.LoadLevel;


            _saveService.SetLevelId(_startData.LevelID);
            _loadData = _saveService.GetLevelData();

            InitPlayer();
            InitCheckPoints();

            _saveService.LoadLevel();
        }

        private void InitPlayer()
        {
            Character character = _characterFactory();

            if (_loadData.PlayerData == null)
            {
                var playerData = new PlayerData
                {
                    PlayerPosition = _gamePoints.StartPoint.transform.position,
                    PlayerRotation = new Quaternion(0, 0, 0, 0)
                };

                //character.transform.SetLocalPositionAndRotation(playerData.PlayerPosition, playerData.PlayerRotation);
                _loadData.PlayerData = playerData;
            }
            else
            {
                var playerData = _loadData.PlayerData;
                _playerData.PlayerPosition = playerData.PlayerPosition;
                _playerData.PlayerRotation = playerData.PlayerRotation;

                //character.transform.SetLocalPositionAndRotation(_playerData.PlayerPosition, _playerData.PlayerRotation);
            }

            _cameraController.SetTarget(character.transform);
            PlayerStateMachine playerStateMachine = _playerStateMachineFactory.Create(character, _cameraController);
            _playerController.SetCharacter(character);
            _playerController.SetPlayerStateMachine(playerStateMachine);
        }

        private void InitCheckPoints() // ПОД ВОПРОСОМ....
        {
            List<CheckPoint> loadCheckPointsData = _loadData.CheckPoints;
            List<CheckPoint> gameCheckPointList = _checkPointsController.TransformToList(_gamePoints.CheckPoints);

            if (loadCheckPointsData == null)
            {
                _checkpointsCount = gameCheckPointList.Count;
                _loadData.CheckPoints = gameCheckPointList;
                
                Debug.Log(_checkpointsCount);
            }
            else
            {
                //_checkPointsController.Load(_loadData); // ПОД ВОПРОСОМ...

                //for(int i = 0; i < gameCheckPointList.Count; i++)
                //{
                //    CheckPoint checkPoint = gameCheckPointList[i];

                //    if(loadCheckPointsData[i].IsActivated == true)
                //    {
                //        checkPoint.Activate();
                //    }
                //    else
                //    {
                //        checkPoint.Deactivate();
                //    }
                //}
            }
        }
    }
}
