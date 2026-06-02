using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using DG.Tweening;
using System.Collections.Generic;
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
        private IGameLogger _gameLogger;

        [Inject]
        public BootEntryPoint(LoadManager loadManager, 
            List<SceneGroupHandle> scensGroups,
            WalletController walletController,
            IGameLogger gameLogger,
            GameSaveLoadService gameSaveLoadService)
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
            _walletController = walletController;
            _gameSaveLoadService = gameSaveLoadService;
            _gameLogger = gameLogger;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);
            
            _gameSaveLoadService.LoadOrCreateSave();

            var data = _gameSaveLoadService.GameSaveData.WalletData;
            _walletModel = new WalletModel(data, _gameLogger);
            _walletController.Initialize(_walletModel);

            await _loadManager.LoadScene(_scensGroups[0]);
        }
    }
}
