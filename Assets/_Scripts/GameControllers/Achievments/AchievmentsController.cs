using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets.Scripts.SaveLoad.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentsController
    {
        private EventBus _eventBus;
        private IGameLogger _gameLogger;

        private AchievmentsCellsView _achievmentsCellsView;

        private List<IAchievement> _modelsAchievments;
        private List<AchievementView> _achievementViews;
        private List<Transform> _cells;
        private int _countAchievmentsMode;

        public AchievmentsController(EventBus eventBus, IGameLogger gameLogger) 
        {
            _eventBus = eventBus;
            _gameLogger = gameLogger;
        }

        public void Initialize(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            _achievementViews = new List<AchievementView>();
            _cells = new List<Transform>();

            _gameLogger.Log("AchievmentsController Initialize", "Success");

            var rewardType1 = new AchievmentsReward(_eventBus);
            rewardType1.AddRevard(new CoinReward(_eventBus, 10));

            var rewardType2 = new AchievmentsReward(_eventBus);
            rewardType2.AddRevard(new GobeletReward(_eventBus, 1));

            var rewardType3 = new AchievmentsReward(_eventBus);
            rewardType3.AddRevard(new CoinReward(_eventBus, 5));
            rewardType3.AddRevard(new GobeletReward(_eventBus, 2));

            AchievmentData achData1 = new AchievmentData() { 
                Id = "1", 
                Name = "sLvl 1", 
                Description = "Start lvl 1", 
                IsUnlock = false, 
                IsClaimed = false };

            AchievmentData achData2 = new AchievmentData()
            {
                Id = "2",
                Name = "sLvl 2",
                Description = "Start lvl 2",
                IsUnlock = false,
                IsClaimed = false
            };

            AchievmentData achData3 = new AchievmentData()
            {
                Id = "3",
                Name = "sLvl 3",
                Description = "Start lvl 3",
                IsUnlock = false,
                IsClaimed = false
            };

            AchievmentData achData4 = new AchievmentData()
            {
                Id = "4",
                Name = "fLvl 1",
                Description = "Finish lvl 1",
                IsUnlock = false,
                IsClaimed = false
            };

            AchievmentData achData5 = new AchievmentData()
            {
                Id = "5",
                Name = "fLvl 2",
                Description = "Finish lvl 2",
                IsUnlock = false,
                IsClaimed = false
            };

            AchievmentData achData6 = new AchievmentData()
            {
                Id = "6",
                Name = "fLvl 3",
                Description = "Finish lvl 3",
                IsUnlock = false,
                IsClaimed = false
            };

            AchievmentData achData7 = new AchievmentData()
            {
                Id = "7",
                Name = "CollectGols",
                Description = "Collect 10 gold",
                IsUnlock = false,
                IsClaimed = false
            };

            AchievmentData achData8 = new AchievmentData()
            {
                Id = "8",
                Name = "Die",
                Description = "Die",
                IsUnlock = false,
                IsClaimed = false
            };

            _modelsAchievments = new List<IAchievement>
                    {
                        new AchievmentModel<StartLevel1>(_eventBus, achData1, rewardType1, _gameLogger),
                        new AchievmentModel<StartLevel2>(_eventBus, achData2, rewardType2, _gameLogger),
                        new AchievmentModel<StartLevel3>(_eventBus, achData3, rewardType3, _gameLogger),
                        new AchievmentModel<FinishLevel1>(_eventBus, achData4, rewardType2, _gameLogger),
                        new AchievmentModel<FinishLevel2>(_eventBus, achData5, rewardType2, _gameLogger),
                        new AchievmentModel<FinishLevel3>(_eventBus, achData6, rewardType3, _gameLogger),
                        new AchievmentModel<CollectGoldEvent>(_eventBus, achData7, rewardType1, _gameLogger),
                        new AchievmentModel<DieEvent>(_eventBus, achData8, rewardType2, _gameLogger),
                    };

            if(gameSaveData.AchievmentsData ==  null || gameSaveData.AchievmentsData.Count == 0)
            {
                gameSaveData.AchievmentsData = new List<AchievmentData>() { achData1, achData2, achData3, achData4, achData5, achData6, achData7, achData8 };
            }

            _countAchievmentsMode = _modelsAchievments.Count;
        }

        public void Dispose()
        {
            _achievementViews.Clear();
            _cells.Clear();
        }

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            List<AchievmentData> achievmentsData = gameSaveData.AchievmentsData;

            for(int i = 0; i < _achievementViews.Count; i++)
            {
                var achModel = _modelsAchievments[i];
                achievmentsData[i] = achModel.GetData();
            }
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            List<AchievmentData> achievmentsData = gameSaveData.AchievmentsData;

            for (int i = 0; i < _modelsAchievments.Count; i++)
            {
                var achModel = _modelsAchievments[i];
                var data = achievmentsData[i];

                achModel.SetData(data);
            }
        }

        public void SetCellView(AchievmentsCellsView achievmentsCellsView)
        {
            _achievmentsCellsView = achievmentsCellsView;
        }

        public void AddAchievmentView(AchievementView achView, int modelIndex)
        {
            if (modelIndex >= _modelsAchievments.Count)
            {
                return;
            }

            if( modelIndex < _countAchievmentsMode )
            {
                var model = _modelsAchievments[modelIndex];
                _achievementViews.Add(achView);
                achView.transform.SetParent(_cells[modelIndex], false);

                model.OnUnlock += () => OnModelUnlocked(model, achView); // а как отписаться ??...
                model.OnChanged += () => OnModelChanged(model, achView);
            }
            else
            {
                throw new System.Exception("_countAchievmentsMode < modelIndex");
            }
        }

        public void AddCell(Transform cell)
        {
            _cells.Add(cell);
        }

        private void OnModelUnlocked(IAchievement model, AchievementView view)
        {
            UpdateView(model, view);
            //_eventBus.Publish(new AchievementUnlockedEvent(model.Data.Id));
        }

        private void OnModelChanged(IAchievement model, AchievementView view)
        {
            // Для прогрессирующих достижений
            UpdateView(model, view);
        }

        private void UpdateView(IAchievement model, AchievementView view)
        {
            var data = model.Data;
            view.SetName(data.Name);
            view.SetDescription(data.Description);

            if (data.IsUnlock)
            {
                view.ShowUnlocked(!data.IsClaimed);
                if (!data.IsClaimed)
                    view.PlayUnlockAnimation();
            }
            else
            {
                view.ShowLocked();
            }
        }

        public void UpdateAllCells()
        {
            //_modelsAchievments
            //_achievementViews;

            for (int i = 0; i < _modelsAchievments.Count; i++) 
            {
                var model = _modelsAchievments[i];
                var view = _achievementViews[i];

                UpdateView(model, view);
            }
        }
    }
}
