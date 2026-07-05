using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointsController : ISave, ILoad, IRestart, IFinish
    {
        private readonly IGameLogger _gameLogger;
        private readonly Transform _objectParent;
        private readonly CheckPointDictinaryModel _dictinaryModel;
        private readonly Dictionary<string, CheckPointView> _dictinaryView;

        public CheckPointsController(GamePoints points, IGameLogger gameLogger, CheckPointDictinaryModel dictinaryModel)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "GamePoints cannot be null");

            _objectParent = points.CheckPoints;
            _dictinaryView = new Dictionary<string, CheckPointView>();
            _gameLogger = gameLogger;
            _dictinaryModel = dictinaryModel;

            _dictinaryModel.OnObjectAdd += ObjectInit;
        }

        public void Dispose()
        {
            _dictinaryModel.OnObjectAdd -= ObjectInit;

            foreach (var view in _dictinaryView.Values)
            {
                view.OnActivateObject -= ObjectActivateView;
                view.OnActivateObject -= TakeObject;

            }

            foreach (var model in _dictinaryModel.ObjectModelds.Values)
            {
                model.OnObjectStatusChange -= OnModelStatusChanged;
            }

            _dictinaryView.Clear();
        }

        public void Initialization(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            var listData = levelData.CheckPoints;

            for (int i = 0; i < _objectParent.childCount; i++)
            {
                //CheckPointView view = null; // под вопросом ... 

                if (_objectParent.GetChild(i).TryGetComponent<CheckPointView>(out var view))
                {


                    // В Этом моменте list data ошибка ArgumentOutOfRangeException
                    // я хочу сделать так. Но как проверить что аргумента i нету в листе??
                    //data = new CheckPointData
                    //{
                    //    Id = model.Key,
                    //    IsActivated = model.Value.IsActivate
                    //}

                    //var data = listData[i];

                    //if (data.Id == null)
                    //{
                    //    string id = GenerateCoinId(i);
                    //    data.Id = id;
                    //}

                    //view.SetId(data.Id);
                    //view.OnActivateObject += ObjectActivateView;
                    //view.OnActivateObject += TakeObject;

                    //_dictinaryModel.AddObject(data);
                    //_dictinaryView[data.Id] = view;
                }
                else
                {
                    throw new ArgumentNullException(nameof(levelData), "view cannot be null/any");
                }
            }
        }

        private void ObjectInit(CheckPointModel model)
        {
            model.OnObjectStatusChange += OnModelStatusChanged;
        }

        private string GenerateCoinId(int index)
        {
            return $"checkPoint_{index}";
        }

        public void ObjectActivateView(string id, bool status)
        {
            if (_dictinaryModel.ObjectModelds.TryGetValue(id, out var model))
            {
                model.SetActivateStatus(status);
                model.Take();
            }
            else
            {
                _gameLogger.LogWarning($"Object with ID {id} not found in models", "Service");
            }
        }

        public void TakeObject(string id, bool status)
        {
            //if (_dictinaryModel.ObjectModelds.TryGetValue(id, out var model))
            //{
            //    model.Take();
            //}
            //else
            //{
            //    _gameLogger.LogWarning($"Object with ID {id} not found in models", "Service");
            //}
        }

        private void OnModelStatusChanged(string id, bool isActivated)
        {
            if (_dictinaryView.TryGetValue(id, out var view))
            {
                view.UpdateView(isActivated);
            }
        }

        public void Finish(LevelData levelData)
        {
            ResetAllObjects();
        }

        public void Restart(LevelData levelData)
        {
            ResetAllObjects();
        }

        public void ResetAllObjects()
        {
            foreach (var model in _dictinaryModel.ObjectModelds.Values)
            {
                model.Reset();
            }
        }

        public void Save(LevelData levelData)
        {
            if (levelData == null) return;

            //levelData.CheckPoints = new List<CheckPointData>();

            ////levelData.LastCheckPointPosition = 

            //foreach (var model in _dictinaryModel.ObjectModelds)
            //{
            //    levelData.CheckPoints.Add(new CheckPointData
            //    {
            //        Id = model.Key,
            //        IsActivated = model.Value.IsActivate
            //    });
            //}

            _gameLogger.Log($"Saved {levelData.CheckPoints.Count} check points", "Service");
        }

        public void Load(LevelData levelData)
        {
            if (levelData?.CheckPoints == null || levelData.CheckPoints.Count == 0)
            {
                _gameLogger.Log("No check point data to load, using defaults");
                return;
            }

            foreach (var objectData in levelData.CheckPoints)
            {
                //if (_dictinaryModel.ObjectModelds.TryGetValue(objectData.Id, out var model))
                //{
                //    model.SetActivateStatus(objectData.IsActivated);
                //}
                //else
                //{
                //    _gameLogger.LogWarning($"Check point with ID {objectData.Id} not found in scene");
                //}
            }

            _gameLogger.Log($"Loaded {levelData.Coins.Count} coins");
        }
    }
}
