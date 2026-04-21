using Assets._Scripts.GameControllers.Achievments;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class GameSaveData
    {
        public Dictionary<string, LevelData> LevelsData = new Dictionary<string, LevelData>();
        public List<AchievmentModel> AchievmentsModels = new List<AchievmentModel>();
        public string CurrentLevelId;
        public DateTime LastSaveTime;
        public int Coins;
        public int Gobelets;
    }
}
