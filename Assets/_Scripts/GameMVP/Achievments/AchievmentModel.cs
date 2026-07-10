using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using System;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Achievments
{
    public interface IAchievement
    {
        public event Action<AchievmentData> OnUnlock;
        public event Action<AchievmentData> OnAchievementDataChanged;

        public AchievmentData Data { get; }

        public AchievmentData GetData();

        public void SetData(AchievmentData data);

        public void TakeReward();

        public void Reset(AchievmentData data);
    }

    public class AchievmentModel
    {
        private Sprite _icon;
        private AchievmentsReward _achievmentsReward;
        private AchievmentData _data;
        private IGameLogger _gameLogger;

        public event Action<AchievmentData> OnUnlock; // открылась ачивка
        public event Action<string, AchievmentData> OnAchievementDataChanged;

        public AchievmentData Data => _data;

        public AchievmentModel(
            AchievmentData data,
            AchievmentsReward achievmentsReward,
            IGameLogger gameLogger)
        {
            _data = data;
            _achievmentsReward = achievmentsReward;
            _gameLogger = gameLogger;
        }


        public void SetData(AchievmentData data)
        {
            _data = data;
            OnAchievementDataChanged?.Invoke(_data.Id, _data);
        }

        public void TakeReward()
        {
            _achievmentsReward.GetRewards();
            _data.IsRevardEnable = false;
            OnAchievementDataChanged?.Invoke(_data.Id, _data);
        }

        public void Reset(AchievmentData data)
        {
            _data = data;
        }

        private void ChangeCurrentProgress()
        {
            //_data.CurrentValue += args.Progress;

            //if (_data.CurrentValue >= _data.TargetValue)
            //{
            //    Unlock();
            //    _eventBus.Unsubscribe<T>(ChangeCurrentProgress);
            //}

            OnAchievementDataChanged?.Invoke(_data.Id, _data);
        }

        private void Unlock()
        {
            if (_data.IsUnlock == false)
            {
                _gameLogger.Log($"Achievment Unlock {_data.Name}", "Achievment");

                _data.IsUnlock = true;
                _data.IsRevardEnable = true;
                OnUnlock?.Invoke(_data);
            }
        }
    }
}
