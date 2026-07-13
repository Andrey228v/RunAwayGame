using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class LevelData
    {
        public int Id;
        public bool IsLevelStart; // Был ли уровень запущен до этого запуска ??
        public int ProgressMax;
        public int CurrentProgress;
        public Vector3 LastCheckPointPosition;
        public PlayerData PlayerData; // под вопросом. Вроде как не используется...
        public Dictionary<string, CheckPointData> CheckPoints;
        public Dictionary<string, CoinData> Coins;

        public LevelData(bool isLevelStart, 
            Vector3 lastCheckPointPosition, 
            PlayerData playerData,
            Dictionary<string, CheckPointData> checkPoints,
            Dictionary<string, CoinData> coins)
        {
            IsLevelStart = isLevelStart;
            LastCheckPointPosition = lastCheckPointPosition;
            PlayerData = playerData;
            CheckPoints = checkPoints;
            Coins = coins;
        }

        public void ResetData(LevelConfig levelConfig)
        {
            LastCheckPointPosition = levelConfig.StartPosition;

            PlayerData.ResetData();

            // Переделать.
            //foreach (var checkPoint in CheckPoints) 
            //{
            //    checkPoint.ResetData();
            //}

            //foreach (var coin in Coins)
            //{
            //    coin.ResetData();
            //}
        }
    }
}
