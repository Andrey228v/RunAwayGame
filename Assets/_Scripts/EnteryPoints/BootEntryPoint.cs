using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class BootEntryPoint : IInitializable
    {
        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;

        public BootEntryPoint(LoadManager loadManager, 
            List<SceneGroupHandle> scensGroups,
            GameSaveLoadService gameSaveLoadService,
            LevelsController levelController,
            AchievmentsController achievmentsController,
            ShopController shopController)
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;

            gameSaveLoadService.AddSerice(levelController);
            gameSaveLoadService.AddSerice(achievmentsController);
            gameSaveLoadService.AddSerice(shopController);
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);

            

            await _loadManager.LoadScene(_scensGroups[0]);
        }
    }
}
