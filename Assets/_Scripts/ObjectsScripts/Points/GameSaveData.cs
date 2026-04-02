using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Points
{
    [Serializable]
    public class GameSaveData
    {
        public Dictionary<int, LevelData> LevelsData = new Dictionary<int, LevelData>();
        public int CurrentLevel;
        public DateTime LastSaveTime;
    }
}
