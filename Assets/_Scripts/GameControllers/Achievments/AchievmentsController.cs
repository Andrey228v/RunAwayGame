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
            _cells = new List<Transform>();
            _achievementViews = new List<AchievementView>();
            _cells = new List<Transform>();
        }

        public void Initialize(AchievmentModelList model)
        {
            _modelsAchievments = model.GetModel();
            _countAchievmentsMode = _modelsAchievments.Count;
        }

        public void Dispose()
        {
            foreach(var view in _achievementViews)
            {
                view.TakeRewardButton.onClick.RemoveAllListeners(); // под вопросом. Если потом удаляются объект то нужны ли отписки...
            }

            _achievementViews.Clear();
            _cells.Clear();

            _achievmentsCellsView.OnDestroyCellsView -= DestroyUI;
        }

        public void SetCellView(AchievmentsCellsView achievmentsCellsView)
        {
            _achievmentsCellsView = achievmentsCellsView;
            _achievmentsCellsView.OnDestroyCellsView += DestroyUI;
        }

        public void SaveAllServices(GameSaveData gameSaveData)
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
                model.OnUpdateView += UpdateCellView;

                achView.TakeRewardButton.onClick.AddListener(model.TakeReward);
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
            //_modelsAchievments = CreateAchievementModels(gameSaveData.AchievmentsData);
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

                if (data.IsUnlock && data.IsRevardEnable)
                {
                    view.ShowUnlockedWithButtonReward();
                }
                else if(data.IsUnlock == false)
                {
                    view.ShowLocked();
                }
                else if(data.IsUnlock && data.IsUnlockAndTaken)
                {
                    view.ShowUnlokedAfterReward();
                }
            }
        }
    }
}
