using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using DG.Tweening;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class BootEntryPoint : IInitializable
    {
        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;
        private WalletModel _walletModel;
        private WalletController _walletController;
        private GameSaveLoadService _gameSaveLoadService;
        private EventBus _eventBus;
        private AchievmentsController _achievmentsController;
        private IGameLogger _gameLogger;

        [Inject]
        public BootEntryPoint(LoadManager loadManager,
            List<SceneGroupHandle> scensGroups,
            WalletController walletController,
            IGameLogger gameLogger,
            EventBus eventBus,
            AchievmentsController achievmentsController,
            GameSaveLoadService gameSaveLoadService)
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
            _walletController = walletController;
            _gameSaveLoadService = gameSaveLoadService;
            _gameLogger = gameLogger;
            _achievmentsController = achievmentsController;
            _eventBus = eventBus;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);
            
            _gameSaveLoadService.LoadOrCreateSave();

            var data = _gameSaveLoadService.GameSaveData.WalletData;
            var achData = _gameSaveLoadService.GameSaveData.AchievmentsData;

            _walletModel = new WalletModel(data, _gameLogger);
            _walletController.Initialize(_walletModel);

            var achModel = new AchievmentModelList(_eventBus, _gameLogger, achData);
            _achievmentsController.Initialize(achModel);

            await _loadManager.LoadScene(_scensGroups[0]);
        }
    }
}
