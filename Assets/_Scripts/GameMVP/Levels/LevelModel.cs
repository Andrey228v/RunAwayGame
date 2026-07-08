using System;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelModel
    {
        public string _id;
        public bool _isLevelStart;
        public int _progressMax;
        public int _currentProgress;

        public event Action OnLevelStart;
        public event Action<int, int> OnProgressChange;
        public event Action<int> OnProgressMaxChange;

        public void SetProgress(int addProgress)
        {
            _currentProgress += addProgress;

            OnProgressChange?.Invoke(_currentProgress, addProgress);
        }

        public void SetLevelStart(bool isLevelStart)
        {
            _isLevelStart = isLevelStart;

            OnLevelStart?.Invoke();
        }

        public void SetProgressMax(int progressMax)
        {
            _progressMax = progressMax;
            OnProgressMaxChange?.Invoke(_progressMax);
        }
    }
}
