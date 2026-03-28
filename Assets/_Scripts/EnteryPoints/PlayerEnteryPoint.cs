using Assets._Scripts.GameControllers;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerEnteryPoint : IStartable, IDisposable
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        private SaveLoadService _saveLoadService;
        private IEnumerable<ISaveLoad> _saveLoads;
        private IEnumerable<IRestart> _restarted;
        private IEnumerable<IFinish> _finished;
        private LevelData _loadData;
        private LevelConfig _levelConfig;
        private GameCycleController _cycleController;
        private GameRestartController _gameRestartController;

        public PlayerEnteryPoint(PlayerController playerController, 
            PlayerStateMachineFactory playerStateMachineFactory, 
            Func<Character> characterFactory, CameraController cameraController, 
            SaveLoadService saveLoadService,
            IEnumerable<ISaveLoad> saveLoads,
            GameCycleController gameCycleController,
            GameRestartController gameRestartController,
            IEnumerable<IRestart> restarted, IEnumerable<IFinish> fineshed) 
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            _saveLoadService = saveLoadService;
            _saveLoads = saveLoads;
            _cycleController = gameCycleController;
            _gameRestartController = gameRestartController;
            _restarted = restarted;
            _finished = fineshed;
        }

        public void Start()
        {
            InitEvents();
            InitSaveLoadData();
        }

        public void Dispose()
        {
            //_cycleController.OnFinishLevel -= RestartPlayer;
        }

        private void InitEvents()
        {
            //_cycleController.OnFinishLevel += RestartPlayer;
        }

        private void InitSaveLoadData()
        {
            _loadData = _saveLoadService.GetLevelData();
            _levelConfig = _saveLoadService.GetLevelConfig();
            _saveLoadService.AddSaveLoadSub(_playerController); // зарегестрировали ISaveLoad надо подумать может передеать по другому...
            _loadData = InitPlayer(_loadData);

            foreach (var data in _saveLoads)
            {
                data.Load(_loadData);
            }
        }

        private LevelData InitPlayer(LevelData levelData)
        {
            Character character = _characterFactory();

            if (levelData.PlayerData == null)
            {
                var playerData = new PlayerData
                {
                    PlayerPosition = _levelConfig.StartPosition,
                    PlayerRotation = Quaternion.Euler(_levelConfig.StartRotationEuler),
                };

                levelData.PlayerData = playerData;
            }

            _cameraController.SetTarget(character.transform);
            PlayerStateMachine playerStateMachine = _playerStateMachineFactory.Create(character, _cameraController);
            _playerController.SetCharacter(character);
            _playerController.SetPlayerStateMachine(playerStateMachine);

            return levelData;
        }

        //private void RestartPlayer()
        //{
        //    _playerController.Restart();

        //    var playerData = new PlayerData
        //    {
        //        PlayerPosition = _levelConfig.StartPosition,
        //        PlayerRotation = Quaternion.Euler(_levelConfig.StartRotationEuler),
        //    };
        //}
    }
}
