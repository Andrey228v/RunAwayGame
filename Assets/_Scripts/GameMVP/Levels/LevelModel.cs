using Assets.Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelModel
    {
        private LevelData _data;

        public event Action OnLevelStart;
        public event Action<int, int> OnProgressChange;
        public event Action<int> OnProgressMaxChange;

        public LevelModel(LevelData data)
        {
            _data = data;
        }

        public void SetProgress(int addProgress)
        {
            _data.CurrentProgress += addProgress;

            OnProgressChange?.Invoke(_data.CurrentProgress, addProgress);
        }

        public void SetLevelStart(bool isLevelStart)
        {
            _data.IsLevelStart = isLevelStart;

            OnLevelStart?.Invoke();
        }

        public void SetProgressMax(int progressMax)
        {
            _data.ProgressMax = progressMax;
            OnProgressMaxChange?.Invoke(_data.ProgressMax);
        }
    }
}
