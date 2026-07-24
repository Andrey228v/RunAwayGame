using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class RootEntryPoint : IInitializable
    {
        private readonly LoadManager _loadManager;
        private readonly List<SceneGroupHandle> _scensGroups;
        private readonly GameLoopService _gameLoopService;
        private readonly LevelsController _levelsController;
        private readonly WalletController _walletController;
        private readonly AchievmentsController _achievmentsController;
        private readonly GameSaveLoadService _gameSaveLoadService;

        [Inject]
        public RootEntryPoint(LoadManager loadManager,
            List<SceneGroupHandle> scensGroups,
            WalletController walletController,
            AchievmentsController achievmentsController,
            LevelsController levelsController,
            GameSaveLoadService gameSaveLoadService,
            GameLoopService gameLoopService
            )
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
            _walletController = walletController;
            _achievmentsController = achievmentsController;
            _levelsController = levelsController;
            _gameLoopService = gameLoopService;
            _gameSaveLoadService = gameSaveLoadService;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);

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

            _levelsController.Initialization(_gameSaveLoadService.GameSaveData);
            _walletController.Initialization(_gameSaveLoadService.GameSaveData);
            _achievmentsController.Initialization(_gameSaveLoadService.GameSaveData);

            await _loadManager.LoadScene(_scensGroups[0]);
        }
    }
}
