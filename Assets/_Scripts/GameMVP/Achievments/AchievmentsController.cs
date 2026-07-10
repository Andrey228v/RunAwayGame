using Assets._Scripts.GameMVP.Achievments;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentsController : ISaveGame, ILoadGame
    {
        private IGameLogger _gameLogger;
        private Transform _objectParent;
        private readonly AchievmentDictinaryModel _dictinaryModel;
        private readonly Dictionary<string, AchievementView> _dictinaryView;

        public AchievmentsController(IGameLogger gameLogger, AchievmentDictinaryModel dictinaryModel) 
        {
            _gameLogger = gameLogger;
            _dictinaryView = new Dictionary<string, AchievementView>();
            _dictinaryModel = dictinaryModel;

            _dictinaryModel.OnObjectAdd += ObjectInit;
        }

        public void Initialization(Transform parent)
        {
            _objectParent = parent;

            for (int i = 0; i < _objectParent.childCount; i++)
            {
                if (_objectParent.GetChild(i).TryGetComponent<AchievementView>(out var view))
                {
                    var id = view.Id;

                    _dictinaryView.Add(id, view);

                    var data = new AchievmentData();
                    var reward = new AchievmentsReward();

                    _dictinaryModel.AddObject(id, data, reward);
                }
            }
        }

        private void ObjectInit(AchievmentModel model)
        {
            model.OnAchievementDataChanged += OnModelStatusChanged;
        }

        public void Dispose()
        {
            //foreach(var view in _achievementViews)
            //{
            //    view.TakeRewardButton.onClick.RemoveAllListeners(); // под вопросом. Если потом удаляются объект то нужны ли отписки...
            //}

            //_achievementViews.Clear();
            //_cells.Clear();

            //_achievmentsCellsView.OnDestroyCellsView -= DestroyUI;
        }

        //public void SetCellView(AchievmentsCellsView achievmentsCellsView)
        //{
        //    _achievmentsCellsView = achievmentsCellsView;
        //    _achievmentsCellsView.OnDestroyCellsView += DestroyUI;
        //}

        //public void AddParent(Transform parent)
        //{
        //    //_objectParent = parent;

        //    //for (int i = 0; i < _objectParent.childCount; i++)
        //    //{
        //    //    if (_objectParent.GetChild(i).TryGetComponent<AchievementView>(out var view))
        //    //    {
        //    //        var id = view.Id;


        //    //    }
        //    //}
        //}

        private void OnModelStatusChanged(string id, AchievmentData data)
        {
            if (_dictinaryView.TryGetValue(id, out var view))
            {
                //view.UpdateView(isActivated);

                view.SetProgress(data);
            }
        }

        public void Save(GameSaveData gameSaveData)
        {
            foreach (var key in gameSaveData.AchievmentsData.Keys)
            {
                if (_dictinaryModel.TryGetModel(key, out var model))
                {
                    gameSaveData.AchievmentsData[key] = model.Data;
                }   
            }
        }

        public void Load(GameSaveData gameSaveData)
        {
            var achievmentData = gameSaveData.AchievmentsData;

            foreach(var key in achievmentData.Keys)
            {
                if(_dictinaryModel.TryGetModel(key, out var model))
                {
                    model.SetData(achievmentData[key]);
                }
            }
        }

        public void AddAchievmentView(AchievementView achView, int modelIndex)
        {
            //if (modelIndex >= _modelsAchievments.Count)
            //{
            //    return;
            //}

            //if( modelIndex < _countAchievmentsMode )
            //{
            //    var model = _modelsAchievments[modelIndex];
            //    _achievementViews.Add(achView);
            //    achView.transform.SetParent(_cells[modelIndex], false);

            //    model.OnUnlock += UpdateCellView;
            //    model.OnAchievementDataChanged += UpdateCellView;

            //    achView.TakeRewardButton.onClick.AddListener(model.TakeReward);

            //    UpdateCellView(model.Data);
            //}
            //else
            //{
            //    throw new System.Exception("_countAchievmentsMode < modelIndex");
            //}
        }

        //public void AddCell(Transform cell)
        //{
        //    _cells.Add(cell);
        //}

        //public void UpdateView()
        //{
        //    if(_achievmentsCellsView != null)
        //    {
        //        for (int i = 0; i < _modelsAchievments.Count; i++)
        //        {
        //            var model = _modelsAchievments[i];
        //            UpdateCellView(model.Data);
        //        }
        //    }
        //}

        public void Reset(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            //_gameLogger.Log("AchievmentsController RESET", "Success");

            //for (int i = 0; i < _modelsAchievments.Count; i++)
            //{
            //    var model = _modelsAchievments[i];
            //    var data = gameSaveData.AchievmentsData[i];

            //    model.Reset(data);

            //    UpdateCellView(data);
            //}
        }

        private void DestroyUI()
        {
            //_gameLogger.Log("DestroyUI AchievmentsCellsView", "Success");

            //for (int i = 0; i < _modelsAchievments.Count; i++)
            //{
            //    var model = _modelsAchievments[i];
            //    var achView = _achievementViews[i];

            //    model.OnUnlock -= UpdateCellView;
            //}
        }

        private void UpdateCellView(AchievmentData achData)
        {
            //if (_achievmentsCellsView != null)
            //{
            //    var model = _modelsAchievments[achData.Id];
            //    var view = _achievementViews[achData.Id];

            //    var data = model.Data;
            //    view.SetName(data.Name);
            //    view.SetDescription(data.Description);

            //    if (data.IsUnlock && data.IsRevardEnable)
            //    {
            //        view.ShowUnlockedWithButtonReward();
            //    }
            //    else if(data.IsUnlock == false)
            //    {
            //        view.ShowLocked();
            //    }
            //    else if(data.IsUnlock && data.IsUnlockAndTaken)
            //    {
            //        view.ShowUnlokedAfterReward();
            //    }

            //    view.SetProgress(achData);
            //}
        }
    }
}
