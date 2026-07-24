using Assets._Scripts.GameMVP.Achievments;
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

        public void Initialization(GameSaveData gameSaveData)
        {
            
        }

        private void ObjectInit(AchievmentModel model)
        {
            model.OnAchievementDataChanged += OnModelStatusChanged;
        }

        public void DisposeMenuView()
        {
            foreach(var key in _dictinaryView.Keys)
            {
                var view = _dictinaryView[key];
                _dictinaryModel.TryGetModel(key, out var model);
                view.OnTakeRewardButtonClick -= model.TakeReward;
            }

            _dictinaryView.Clear();
        }

        public void AddMenuView(Transform parent)
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

                    _dictinaryModel.TryAddObject(id, data, reward); // под вопросом... почему тут Трай, а это никак не используется.
                    _dictinaryModel.TryGetModel(id, out var model); // под вопросом. Тут тоже трай не понятно зачем.
                    view.OnTakeRewardButtonClick += model.TakeReward;
                }
            }
        }

        private void OnModelStatusChanged(string id, AchievmentData data)
        {
            if (_dictinaryView.TryGetValue(id, out var view))
            {
                view.SetDataView(data);
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

    }
}
