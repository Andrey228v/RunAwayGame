using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets.Scripts.SaveLoad;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabs> _menuFactory;
        private Func<AchievmentsCellsView> _achievmentsCellsFactory;
        private Func<AchievementView> _achievmentsViewFactory;

        private SaveLoadService _saveLoadService;
        private MenuTabs _menu;
        private AchievmentsCellsView _achievments;
        private GameManager _gameManger;

        private List<AchievmentModel> _achievmentsModels;
        private List<AchievementView> _achievementViews;

        public MenuEnteryPoint(Func<MenuTabs> menuFactory, 
            SaveLoadService saveLoadService,
            Func<AchievmentsCellsView> achievmentsCellsFactory,
            Func<AchievementView> achievmentsViewFactory,
            GameManager gameManager) 
        {
            _menuFactory = menuFactory;
            _saveLoadService = saveLoadService;
            _achievmentsCellsFactory = achievmentsCellsFactory;
            _achievmentsViewFactory = achievmentsViewFactory;
            _gameManger = gameManager;

            _achievmentsModels = _saveLoadService.SaveData.AchievmentsModels; // Переделать ...
            _achievementViews = new List<AchievementView>();
        }

        public void Start()
        {

            if(_achievmentsModels.Count == 0)
            {
                _saveLoadService.SaveData.AchievmentsModels = new List<AchievmentModel>
                    {
                        new AchievmentModel("sLvl 1", "Start lvl 1", false, false),
                        new AchievmentModel("sLvl 2", "Start lvl 2", false, false),
                        new AchievmentModel("sLvl 3", "Start lvl 3", false, false),
                        new AchievmentModel("fLvl 1", "Finish lvl 1", false, false),
                        new AchievmentModel("fLvl 2", "Finish lvl 2", false, false),
                        new AchievmentModel("fLvl 3", "Finish lvl 3", false, false),
                    };

                
            }


            _saveLoadService.SaveGameData();

            InitMenu();
            InitAchievments();
        }

        public void Dispose()
        {
            _menu.OnChooseLevel -= _saveLoadService.SetLevelConfig;
            _menu.OnSaveDelet -= _saveLoadService.ResetAllProgress;

            Debug.Log("DISPOSE MEP");

            _gameManger.OnLevelStart0 -= _achievementViews[0].Unlock;
            _gameManger.OnLevelStart0 -= _saveLoadService.SaveData.AchievmentsModels[0].Unlock;

            _gameManger.OnLevelStart1 -= _achievementViews[1].Unlock;
            _gameManger.OnLevelStart1 -= _saveLoadService.SaveData.AchievmentsModels[1].Unlock;

            _gameManger.OnLevelStart2 -= _achievementViews[2].Unlock;
            _gameManger.OnLevelStart2 -= _saveLoadService.SaveData.AchievmentsModels[2].Unlock;

            _gameManger.OnLevelFinish0 -= _achievementViews[3].Unlock;
            _gameManger.OnLevelFinish0 -= _saveLoadService.SaveData.AchievmentsModels[3].Unlock;

            _gameManger.OnLevelFinish1 -= _achievementViews[4].Unlock;
            _gameManger.OnLevelFinish1 -= _saveLoadService.SaveData.AchievmentsModels[4].Unlock;

            _gameManger.OnLevelFinish2 -= _achievementViews[5].Unlock;
            _gameManger.OnLevelFinish2 -= _saveLoadService.SaveData.AchievmentsModels[5].Unlock;

            _saveLoadService.SaveGameData();
        }

        public void InitMenu()
        {
            _menu = _menuFactory();
            _achievments = _achievmentsCellsFactory();
            _achievments.transform.SetParent(_menu.AchievmentsParent, false);
            //int counter = 0;

            foreach(AchievmentModel ach in _achievmentsModels)
            {
                var achView = _achievmentsViewFactory();
                achView.Construct(ach);
                _achievments.AddAchievment(achView);

                _achievementViews.Add(achView);

                //if(ach.IsUnlock == false)
                //{
                //    _actions[counter] += achView.Unlock;
                //    _actions[counter] += ach.Unlock;
                //}



            }


            _menu.OnChooseLevel += _saveLoadService.SetLevelConfig;
            _menu.OnSaveDelet += _saveLoadService.ResetAllProgress;
        }

        public void InitAchievments()
        {
            _gameManger.OnLevelStart0 += _achievementViews[0].Unlock;
            _gameManger.OnLevelStart0 += _saveLoadService.SaveData.AchievmentsModels[0].Unlock;

            _gameManger.OnLevelStart1 += _achievementViews[1].Unlock;
            _gameManger.OnLevelStart1 += _saveLoadService.SaveData.AchievmentsModels[1].Unlock;

            _gameManger.OnLevelStart2 += _achievementViews[2].Unlock;
            _gameManger.OnLevelStart2 += _saveLoadService.SaveData.AchievmentsModels[2].Unlock;

            _gameManger.OnLevelFinish0 += _achievementViews[3].Unlock;
            _gameManger.OnLevelFinish0 += _saveLoadService.SaveData.AchievmentsModels[3].Unlock;

            _gameManger.OnLevelFinish1 += _achievementViews[4].Unlock;
            _gameManger.OnLevelFinish1 += _saveLoadService.SaveData.AchievmentsModels[4].Unlock;

            _gameManger.OnLevelFinish2 += _achievementViews[5].Unlock;
            _gameManger.OnLevelFinish2 += _saveLoadService.SaveData.AchievmentsModels[5].Unlock;
        }
    }
}
