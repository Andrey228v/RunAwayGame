//using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
//using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
//using Assets._Scripts.Utilites.Loger;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class RootEntryPoint : IInitializable
    {
        //private GameSaveLoadService _gameSaveLoadService;
        private readonly LoadManager _loadManager;
        private readonly List<SceneGroupHandle> _scensGroups;
        //private WalletModel _walletModel;
        private readonly GameLoopService _gameLoopService;
        private readonly LevelsController _levelsController;
        private readonly WalletController _walletController;
        private readonly AchievmentsController _achievmentsController;
        //private IGameLogger _gameLogger;
        //private EventBus _eventBus;

        [Inject]
        public RootEntryPoint(LoadManager loadManager,
            List<SceneGroupHandle> scensGroups,
            WalletController walletController,
            //IGameLogger gameLogger,
            //EventBus eventBus,
            AchievmentsController achievmentsController,
            LevelsController levelsController,
            GameLoopService gameLoopService
            )
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
            _walletController = walletController;
            //_gameSaveLoadService = gameSaveLoadService;
            //_gameLogger = gameLogger;
            _achievmentsController = achievmentsController;
            _levelsController = levelsController;
            //_eventBus = eventBus;
            _gameLoopService = gameLoopService;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);

            //var gameSaveData = _gameSaveLoadService.GameSaveData;
            //var data = _gameSaveLoadService.GameSaveData.WalletData;
            //var achData = _gameSaveLoadService.GameSaveData.AchievmentsData;

            _gameLoopService.SaveDict.Add("levelsController", _levelsController);
            _gameLoopService.LoadDict.Add("levelsController", _levelsController);
            _gameLoopService.DieRestartDict.Add("levelsController", _levelsController);
            _gameLoopService.FinishDict.Add("levelsController", _levelsController);
            _gameLoopService.ResetDict.Add("levelsController", _levelsController);

            _gameLoopService.SaveDict.Add("waletController", _walletController);
            _gameLoopService.LoadDict.Add("waletController", _walletController);
            _gameLoopService.FinishDict.Add("waletController", _walletController);
            _gameLoopService.ResetDict.Add("waletController", _walletController);

            _gameLoopService.SaveDict.Add("achievmentsController", _achievmentsController);
            _gameLoopService.LoadDict.Add("achievmentsController", _achievmentsController);

            //_levelsController.Initialization(gameSaveData);
            //_walletController.Initialization(gameSaveData);
            //_achievmentsController.Initialization(gameSaveData);

            //_levelsController.Load(gameSaveData);
            //_walletController.Load(gameSaveData);
            //_achievmentsController.Load(gameSaveData);


            //var achModel = new AchievmentModelList(_eventBus, _gameLogger, achData, _walletController);
            //_achievmentsController.Initialize(achModel);


            await _loadManager.LoadScene(_scensGroups[0]);
        }
    }
}
