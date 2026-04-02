using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Points;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class LevelData
    {
        public string LevelID;
        public bool IsLevelWasStarted; // Был ли уровень запущен до этого запуска ??
        public Transform LastCheckPointPosition;
        public PlayerData PlayerData;
        public List<CheckPointData> CheckPoints;
        public List<CoinData> Coins;
    }
}
