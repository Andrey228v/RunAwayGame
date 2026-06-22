using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Achievments
{
    public interface IAchievement
    {
        public event Action<AchievmentData> OnUnlock;
        //public event Action<int> OnUpdateView;
        public event Action<AchievmentData> OnAchievementDataChanged;

        public AchievmentData Data { get; }

        public AchievmentData GetData();

        public void SetData(AchievmentData data);

        public void TakeReward();

        public void Reset(AchievmentData data);
    }

    public class AchievmentModel<T>: IAchievement where T : struct, IAchivmentEvent
    {
        private Sprite _icon;
        private AchievmentsReward _achievmentsReward;
        private EventBus _eventBus;
        private AchievmentData _data;
        private IGameLogger _gameLogger;

        public event Action<AchievmentData> OnUnlock; // открылась ачивка
        //public event Action<int> OnUpdateView;
        public event Action<AchievmentData> OnAchievementDataChanged;

        public AchievmentData Data => _data;

        public AchievmentModel(EventBus eventBus,
            AchievmentData data,
            AchievmentsReward achievmentsReward,
            IGameLogger gameLogger)
        {
            _eventBus = eventBus;
            _data = data;
            _achievmentsReward = achievmentsReward;
            _gameLogger = gameLogger;

            if(_data.IsUnlock == false)
            {
                _eventBus.Subscribe<T>(ChangeCurrentProgress);
            }
        }

        public AchievmentData GetData()
        {
            return _data;
        }

        public void SetData(AchievmentData data)
        {
            _data = data;
            OnAchievementDataChanged?.Invoke(_data);
        }

        public void TakeReward()
        {
            _achievmentsReward.GetRewards();
            _data.IsRevardEnable = false;
            OnAchievementDataChanged?.Invoke(_data);
            //OnUpdateView?.Invoke(_data.Id);
        }

        public void Reset(AchievmentData data)
        {
            _data = data;
            _eventBus.Subscribe<T>(ChangeCurrentProgress);
        }

        private void ChangeCurrentProgress(T args)
        {
            _data.CurrentValue += args.Progress;

            if (_data.CurrentValue >= _data.TargetValue)
            {
                Unlock();
                _eventBus.Unsubscribe<T>(ChangeCurrentProgress);
            }

            OnAchievementDataChanged?.Invoke(_data);
        }

        private void Unlock()
        {
            if (_data.IsUnlock == false)
            {
                _gameLogger.Log($"Achievment Unlock {_data.Name}", "Achievment");

                _data.IsUnlock = true;
                _data.IsRevardEnable = true;
                OnUnlock?.Invoke(_data);

                _eventBus.Publish(new SaveGameEvent());
            }
        }
    }
}
