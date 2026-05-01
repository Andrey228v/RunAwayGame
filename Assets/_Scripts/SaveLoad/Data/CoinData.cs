using System;

namespace Assets._Scripts.SaveLoad.Data
{
    [Serializable]
    public class CoinData
    {
        public bool IsActivated;

        public void ResetData(LevelConfig levelConfig)
        {
            IsActivated = false;
        }
    }
}
