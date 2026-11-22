using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Settings;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.GameMVP.Language;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using Assets._Scripts.UI._1MenuWindow.Language;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class RootEntryPoint : IInitializable, IDisposable
    {
        private readonly LoadManager _loadManager;
        private readonly List<SceneGroupHandle> _scensGroups;
        private readonly GameLoopService _gameLoopService;
        private readonly LevelsController _levelsController;
        private readonly WalletController _walletController;
        private readonly AchievmentsController _achievmentsController;
        private readonly GameSaveLoadService _gameSaveLoadService;
        private readonly LanguageController _languageController;
        private readonly LanguageModel _languageModel;
        private readonly LanguageManger _languageManger;
        private readonly SettingsController _settingsController;
        private readonly SettingsModel _settingsModel;

        [Inject]
        public RootEntryPoint(LoadManager loadManager,
            List<SceneGroupHandle> scensGroups,
            WalletController walletController,
            AchievmentsController achievmentsController,
            LevelsController levelsController,
            GameSaveLoadService gameSaveLoadService,
            LanguageController languageController,
            LanguageModel languageModel,
            LanguageManger languageManger,
            SettingsController settingsController,
            SettingsModel settingsModel,
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
            _languageController = languageController;
            _languageModel = languageModel;
            _languageManger = languageManger;
            _settingsController = settingsController;
            _settingsModel = settingsModel;
        }

        public void Dispose()
        {
            _languageModel.OnChangedForSave -= SaveLevel;
            _settingsModel.OnChangedForSave -= SaveLevel;
            _languageModel.OnLangageIdChanged -= _languageManger.SetLanguageId; // было SetLanguage
            _languageController.Dispose();
            _settingsController.Dispose();

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
            _gameLoopService.ResetDict.Add("achievmentsController", _achievmentsController);

            _gameLoopService.SaveDict.Add("languageController", _languageController);
            _gameLoopService.LoadDict.Add("languageController", _languageController);
            _gameLoopService.ResetDict.Add("languageController", _languageController);

            _gameLoopService.SaveDict.Add("settingsController", _settingsController);
            _gameLoopService.LoadDict.Add("settingsController", _settingsController);
            _gameLoopService.ResetDict.Add("settingsController", _settingsController);

            _walletController.Initialization(_gameSaveLoadService.GameSaveData);
            _achievmentsController.Initialization(_gameSaveLoadService.GameSaveData);
            _languageController.Initialization(_gameSaveLoadService.GameSaveData);
            _languageManger.Initialization(_gameSaveLoadService.GameSaveData);
            _settingsController.Initialization(_gameSaveLoadService.GameSaveData);

            _languageModel.OnChangedForSave += SaveLevel;
            _languageModel.OnLangageIdChanged += _languageManger.SetLanguageId; // было SetLanguage

            _settingsModel.OnChangedForSave += SaveLevel;


            await _loadManager.LoadScene(_scensGroups[0]);
        }

        //Сохранаяем при взятии чекпоинта, монетки, завершении уровня.
        private void SaveLevel()
        {
            _gameLoopService.SaveAllServices(_gameSaveLoadService.GameSaveData);
        }

        //private void SetLanguage(int id)
        //{
        //    _languageManger.SetLanguageId(id);
        //}
    }
}
