using Assets._Scripts.EnteryPoints.Interfaces;
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
    public class PlayerEnteryPoint : IStartable, IDisposable, IInitSaveLoad, IInitFinish, IInitRestart
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        private SaveLoadService _saveLoadService;
        private LevelData _loadData;
        private LevelConfig _levelConfig;
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;

        public IEnumerable<ISaveLoad> SaveLoads { get; private set; }

        public IEnumerable<IFinish> Finished { get; private set; }

        public IEnumerable<IRestart> Restarted { get; private set; }

        public PlayerEnteryPoint(PlayerController playerController, 
            PlayerStateMachineFactory playerStateMachineFactory, 
            Func<Character> characterFactory, CameraController cameraController, 
            SaveLoadService saveLoadService,
            IEnumerable<ISaveLoad> saveLoads,
            GameFinishController gameFinishController,
            GameRestartController gameRestartController,
            IEnumerable<IRestart> restarted, IEnumerable<IFinish> fineshed) 
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            _saveLoadService = saveLoadService;
            _finishController = gameFinishController;
            _gameRestartController = gameRestartController;
            SaveLoads = saveLoads;
            Finished = fineshed;
            Restarted = restarted;
        }

        public void Start()
        {
            InitEvents();
            InitSaveLoadData();
            InitFinishData();
            InitRestartData();
        }

        public void Dispose()
        {
            //_cycleController.OnFinishLevel -= RestartPlayer;
        }

        private void InitEvents()
        {
            //_cycleController.OnFinishLevel += RestartPlayer;
        }

        public void InitSaveLoadData()
        {
            _loadData = _saveLoadService.GetLevelData();
            _levelConfig = _saveLoadService.GetLevelConfig();
            _saveLoadService.AddSaveLoadSub(_playerController); // зарегестрировали ISaveLoad надо подумать может передеать по другому...
            _loadData = InitPlayer(_loadData);

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

        private LevelData InitPlayer(LevelData levelData)
        {
            Character character = _characterFactory();

            _cameraController.SetTarget(character.transform);
            PlayerStateMachine playerStateMachine = _playerStateMachineFactory.Create(character, _cameraController);
            _playerController.SetCharacter(character);
            _playerController.SetPlayerStateMachine(playerStateMachine);

            return levelData;
        }
    }
}
