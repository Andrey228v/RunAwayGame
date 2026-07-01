using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinController:  ISave, ILoad, IRestart, IFinish
    {
        private Transform _objectParent;
        private List<CoinView> _objectList;
        private List<CoinData> _objectData;
        private List<CoinModel> _objectModels;

        public event Action<int> OnTake;

        public CoinController(GamePoints points)
        {
            if (points != null)
                _objectParent = points.Coins;
            else
                throw new ArgumentNullException(nameof(points), "GamePoints cannot be null");

            _objectList = TransformToList(_objectParent);
        }

        public void Dispose()
        {
            foreach (CoinView coinView in _objectList)
            {
                coinView.OnActivateObject -= CoinActivated;
            }
        }

        public List<CoinView> TransformToList(Transform objectsParent)
        {
            if (objectsParent == null)
                throw new ArgumentNullException(nameof(objectsParent), "coinParent cannot be null");

            List<CoinView> Coins = new List<CoinView>();

            for (int i = 0; i < objectsParent.childCount; i++)
            {
                CoinView coinView = objectsParent.GetChild(i).GetComponent<CoinView>();
                Coins.Add(coinView);
                coinView.OnActivateObject += CoinActivated;

                CoinModel coinModel = new CoinModel(); // Переделать. Вывести в отдельную функцию...
                _objectModels.Add(coinModel);

            }

            return Coins;
        }

        public void CoinActivated(bool status)
        {
            //Переделать ... ...
            //OnTake?.Invoke(1);


        }

        public void Finish(LevelData levelData)
        {
            //Restart(levelData);
        }

        public void Restart(LevelData levelData)
        {
            foreach (var obj in _objectList)
            {
                //obj.Deactivate();
            }
        }

        public void Save(LevelData levelData)
        {
            //for (int i = 0; i < _objectList.Count; i++)
            //{
            //    levelData.Coins[i] = new CoinData { IsActivated = _objectList[i].IsActivated };
            //}
        }

        public void Load(LevelData levelData)
        {
            //var objectCount = _objectList.Count;

            //if (levelData.Coins == null || levelData.Coins.Count == 0)
            //{
            //    _objectData = new List<CoinData>();

            //    for (int i = 0; i < _objectList.Count; i++)
            //    {
            //        _objectData.Add(new CoinData { IsActivated = _objectList[i].IsActivated });
            //    }

            //    levelData.Coins = _objectData;
            //}
            //else
            //{
            //    for (int i = 0; i < objectCount; i++)
            //    {
            //        if (_objectList[i].IsActivated == false)
            //        {
            //            CoinView obj = _objectList[i];
            //            CoinData objData = levelData.Coins[i];
            //            obj.SetState(objData.IsActivated);
            //        }
            //    }
            //}
        }
    }
}
