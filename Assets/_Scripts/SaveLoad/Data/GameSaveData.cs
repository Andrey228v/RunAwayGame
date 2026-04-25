using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class GameSaveData
    {
        public Dictionary<string, LevelData> LevelsData = new Dictionary<string, LevelData>(); // LevelController
        public AchievmentsData AchievmentsData; // AchievmentsController
        public ShopData ShopData; // ShopController
        public DateTime LastSaveTime;
    }
}
