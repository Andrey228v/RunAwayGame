using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabs> _menuFactory;
        private Func<AchievmentsCellsView> _achievmentsCellsFactory;
        private Func<AchievementView> _achievmentsViewFactory;

        private MenuTabs _menuTabs;
        private AchievmentsCellsView _achievments;
        private GameManager _gameManger;

        private GameSaveLoadService _gameSaveLoadService;
        private List<AchievementView> _achievementViews;
        //private GameSaveData _gameSaveData;

        public MenuEnteryPoint(Func<MenuTabs> menuFactory,
            Func<AchievmentsCellsView> achievmentsCellsFactory,
            Func<AchievementView> achievmentsViewFactory,
            GameManager gameManager,
            GameSaveLoadService gameSaveLoadService) 
        {
            _menuFactory = menuFactory;
            _achievmentsCellsFactory = achievmentsCellsFactory;
            _achievmentsViewFactory = achievmentsViewFactory;
            _gameManger = gameManager;

            _achievementViews = new List<AchievementView>();

            _gameSaveLoadService = gameSaveLoadService;
            //_gameSaveData = gameSaveLoadService.GameSaveData;
        }

        public void Start()
        {
            InitMenu();
            InitAchievments();
        }

        public void Dispose()
        {
            _menuTabs.OnChooseLevel -= _gameSaveLoadService.SetLevelConfig;
            _menuTabs.OnSaveDelet -= _gameSaveLoadService.ResetAllProgress;

            //_menu.OnChooseLevel -= _saveLoadService.SetLevelConfig;
            //_menu.OnSaveDelet -= _saveLoadService.ResetAllProgress;

            //Debug.Log("DISPOSE MEP");

            //_gameManger.OnLevelStart0 -= _achievementViews[0].Unlock;
            //_gameManger.OnLevelStart0 -= _saveLoadService.SaveData.AchievmentsModels[0].Unlock;

            //_gameManger.OnLevelStart1 -= _achievementViews[1].Unlock;
            //_gameManger.OnLevelStart1 -= _saveLoadService.SaveData.AchievmentsModels[1].Unlock;

            //_gameManger.OnLevelStart2 -= _achievementViews[2].Unlock;
            //_gameManger.OnLevelStart2 -= _saveLoadService.SaveData.AchievmentsModels[2].Unlock;

            //_gameManger.OnLevelFinish0 -= _achievementViews[3].Unlock;
            //_gameManger.OnLevelFinish0 -= _saveLoadService.SaveData.AchievmentsModels[3].Unlock;

            //_gameManger.OnLevelFinish1 -= _achievementViews[4].Unlock;
            //_gameManger.OnLevelFinish1 -= _saveLoadService.SaveData.AchievmentsModels[4].Unlock;

            //_gameManger.OnLevelFinish2 -= _achievementViews[5].Unlock;
            //_gameManger.OnLevelFinish2 -= _saveLoadService.SaveData.AchievmentsModels[5].Unlock;

            //_saveLoadService.SaveGameData();
        }

        public void InitMenu()
        {
            _menuTabs = _menuFactory();
            _achievments = _achievmentsCellsFactory();
            _achievments.transform.SetParent(_menuTabs.AchievmentsParent, false);

            _menuTabs.OnChooseLevel += _gameSaveLoadService.SetLevelConfig; // убрать ???...
            _menuTabs.OnSaveDelet += _gameSaveLoadService.ResetAllProgress;
            //_menuTabs.OnChooseLevel += _levelController.SetLevelConfig;
        }

        public void InitAchievments()
        {
            //foreach (AchievmentModel ach in _gameSaveData.AchievmentsModels)
            //{
            //    var achView = _achievmentsViewFactory();
            //    achView.Construct(ach);
            //    _achievments.AddAchievment(achView);

            //    _achievementViews.Add(achView);
            //}


            //_gameManger.OnLevelStart0 += _achievementViews[0].Unlock;
            //_gameManger.OnLevelStart0 += _saveLoadService.SaveData.AchievmentsModels[0].Unlock;

            //_gameManger.OnLevelStart1 += _achievementViews[1].Unlock;
            //_gameManger.OnLevelStart1 += _saveLoadService.SaveData.AchievmentsModels[1].Unlock;

            //_gameManger.OnLevelStart2 += _achievementViews[2].Unlock;
            //_gameManger.OnLevelStart2 += _saveLoadService.SaveData.AchievmentsModels[2].Unlock;

            //_gameManger.OnLevelFinish0 += _achievementViews[3].Unlock;
            //_gameManger.OnLevelFinish0 += _saveLoadService.SaveData.AchievmentsModels[3].Unlock;

            //_gameManger.OnLevelFinish1 += _achievementViews[4].Unlock;
            //_gameManger.OnLevelFinish1 += _saveLoadService.SaveData.AchievmentsModels[4].Unlock;

            //_gameManger.OnLevelFinish2 += _achievementViews[5].Unlock;
            //_gameManger.OnLevelFinish2 += _saveLoadService.SaveData.AchievmentsModels[5].Unlock;
        }
    }
}
