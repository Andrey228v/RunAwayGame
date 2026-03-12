using Assets.Scripts.Points;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class LevelData
    {
        public string LevelID;
        public bool IsLevelWasStarted; // Был ли уровень запущен до этого запуска ??
        public CheckPoint LastCheckpointPosition;
        public PlayerData PlayerData;
        public List<CheckPointData> CheckPoints;
        public bool IsLevelCompleted;
    }
}
