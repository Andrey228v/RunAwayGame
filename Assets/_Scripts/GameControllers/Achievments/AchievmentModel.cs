using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Achievments
{
    public interface IAchievement
    {
        public event Action<int> OnUnlock;
        public event Action<int> OnChanged;
        public event Action<int> OnUpdateView;

        public AchievmentData Data { get; }

        public AchievmentData GetData();

        public void SetData(AchievmentData data);

        public void TakeReward();
    }


    public class AchievmentModel<T>: IAchievement where T : struct
    {
        private Sprite _icon;
        private AchievmentsReward _achievmentsReward;
        private EventBus _eventBus;
        private AchievmentData _data;
        private IGameLogger _gameLogger;

        public event Action<int> OnUnlock;
        public event Action<int> OnChanged;
        public event Action<int> OnUpdateView;

        public AchievmentData Data => _data;

        public AchievmentModel(EventBus eventBus,
            AchievmentData data,
            AchievmentsReward achievmentsReward,
            IGameLogger gameLogger)
        {
            _eventBus = eventBus;
            _data = data;
            _achievmentsReward = achievmentsReward;
            _eventBus.Subscribe<T>(Unlock);
            _gameLogger = gameLogger;
        }

        public void Unlock(T args)
        {
            if(_data.IsUnlock == false)
            {
                _gameLogger.Log($"Achievment Unlock {_data.Name}", "Achievment");

                _data.IsUnlock = true;
                _data.IsRevardEnable = true;
                OnUnlock?.Invoke(_data.Id);

                _eventBus.Publish(new SaveGameEvent());
            }
        }

        public AchievmentData GetData()
        {
            return _data;
        }

        public void SetData(AchievmentData data)
        {
            _data = data;
        }

        public void TakeReward()
        {
            _achievmentsReward.GetRewards();
            _data.IsRevardEnable = false;
            OnUpdateView?.Invoke(_data.Id);
        }
    }
}
