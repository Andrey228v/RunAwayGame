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
        private List<Transform> _cells;
        private List<AchievementView> _achievementViews;
        private int _countAchievmentsMode;

        public AchievmentsController(EventBus eventBus, IGameLogger gameLogger) 
        {
            _eventBus = eventBus;
            _gameLogger = gameLogger;
            _modelsAchievments = new List<IAchievement>();
            _cells = new List<Transform>();
            _achievementViews = new List<AchievementView>();
        }

        public void Initialize(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            _achievementViews = new List<AchievementView>();
            _cells = new List<Transform>();

            _gameLogger.Log("AchievmentsController Initialize", "Success");
            _modelsAchievments = CreateAchievementModels(gameSaveData.AchievmentsData);
            _countAchievmentsMode = _modelsAchievments.Count;
        }

        public void Dispose()
        {
            _achievementViews.Clear();
            _cells.Clear();

            _achievmentsCellsView.OnDestroyCellsView -= DestroyUI;
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
            _achievmentsCellsView.OnDestroyCellsView += DestroyUI;
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

                model.OnUnlock += UpdateCellView;
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

        public void UpdateView()
        {
            if(_achievmentsCellsView != null)
            {
                for (int i = 0; i < _modelsAchievments.Count; i++)
                {
                    var model = _modelsAchievments[i];
                    UpdateCellView(model.Data.Id);
                }
            }
        }

        public void Reset(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            _gameLogger.Log("AchievmentsController RESET", "Success");
            _modelsAchievments = CreateAchievementModels(gameSaveData.AchievmentsData);
            _countAchievmentsMode = _modelsAchievments.Count;
        }

        private void DestroyUI()
        {
            _gameLogger.Log("DestroyUI AchievmentsCellsView", "Success");

            foreach (var model in _modelsAchievments)
            {
                model.OnUnlock -= UpdateCellView;
            }
        }

        private void UpdateCellView(int id)
        {
            if (_achievmentsCellsView != null)
            {
                var model = _modelsAchievments[id];
                var view = _achievementViews[id];

                var data = model.Data;
                view.SetName(data.Name);
                view.SetDescription(data.Description);

                if (data.IsUnlock)
                {
                    view.ShowUnlocked(data.IsClaimed == false);
                    if (data.IsClaimed == false)
                        view.PlayUnlockAnimation();
                }
                else
                {
                    view.ShowLocked();
                }
            }
        }

        private List<IAchievement> CreateAchievementModels(List<AchievmentData> achievmentData)
        {
            if (_modelsAchievments == null || _eventBus == null || _gameLogger == null)
            {
                throw new System.Exception("_modelsAchievments == null || _eventBus == null || _gameLogger == null");
            }

            var rewardType1 = new AchievmentsReward(_eventBus);
            rewardType1.AddRevard(new CoinReward(_eventBus, 10));

            var rewardType2 = new AchievmentsReward(_eventBus);
            rewardType2.AddRevard(new GobeletReward(_eventBus, 1));

            var rewardType3 = new AchievmentsReward(_eventBus);
            rewardType3.AddRevard(new CoinReward(_eventBus, 5));
            rewardType3.AddRevard(new GobeletReward(_eventBus, 2));

            _modelsAchievments = new List<IAchievement>
                    {
                        new AchievmentModel<StartLevel1>(_eventBus, achievmentData[0], rewardType1, _gameLogger),
                        new AchievmentModel<StartLevel2>(_eventBus, achievmentData[1], rewardType2, _gameLogger),
                        new AchievmentModel<StartLevel3>(_eventBus, achievmentData[2], rewardType3, _gameLogger),
                        new AchievmentModel<FinishLevel1>(_eventBus, achievmentData[3], rewardType2, _gameLogger),
                        new AchievmentModel<FinishLevel2>(_eventBus, achievmentData[4], rewardType2, _gameLogger),
                        new AchievmentModel<FinishLevel3>(_eventBus, achievmentData[5], rewardType3, _gameLogger),
                        new AchievmentModel<CollectGoldEvent>(_eventBus, achievmentData[6], rewardType1, _gameLogger),
                        new AchievmentModel<DieEvent>(_eventBus, achievmentData[7], rewardType2, _gameLogger),
                    };

            return _modelsAchievments;
        }
    }
}
