using Assets._Scripts.EventBusGame;
using Assets.Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentModel<T>: IAchievement where T : struct
    {
        private string _id;
        private string _name;
        private string _description;
        private bool _isUnlock;
        private Sprite _icon;
        private int _targetValue;
        private int _currentValue;
        private bool _isClaimed;
        private AchievmentsReward _achievmentsReward;
        private EventBus _eventBus;

        public event Action OnUnlock;
        public event Action OnChanged;

        public string Id => _id;
        public string Name => _name;
        public bool IsUnlocked => _isUnlock;
        public bool IsClaimed => _isClaimed;
        public float Progress => (float)_currentValue / _targetValue;
        public string Description => _description;
        public bool IsUnlock => _isUnlock;
        public bool CanClaim => _isUnlock && !_isClaimed;

        public AchievmentModel(EventBus eventBus, 
            string name, 
            string description, 
            bool isUnlock, 
            bool isClaimed, 
            AchievmentsReward achievmentsReward)
        {
            _eventBus = eventBus;
            _name = name;
            _description = description;
            _isUnlock = isUnlock;
            _isClaimed = isClaimed;
            _achievmentsReward = achievmentsReward;
            _eventBus.Subscribe<T>(Unlock);
        }

        public void SetUnlock(bool isUnlock) 
        {
            _isUnlock = isUnlock;
        }

        public void Unlock(T args)
        {
            _isUnlock = true;
            _achievmentsReward.GetRewards();

            OnUnlock?.Invoke();
        }

        public void Save(GameSaveData gameSaveData, LevelConfig levelConfig)
        {

        }

        public void Load(GameSaveData gameSaveData, LevelConfig levelConfig)
        {

        }

        public void ClaimReward()
        {
            
        }

        //картинку сделать..

        //private Func<bool> _


    }
}
