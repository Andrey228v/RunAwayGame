using Assets._Scripts.EventBusGame;
using Assets.Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentModel
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
        private Action _action;
        private EventBus _eventBus;

        public float Progress => (float)_currentValue / _targetValue;
        public string Name => _name;
        public string Description => _description;
        public bool IsUnlock => _isUnlock;
        public bool CanClaim => _isUnlock && !_isClaimed;

        public AchievmentModel(EventBus eventBus, string name, string description, bool isUnlock, bool isClaimed, AchievmentsReward achievmentsReward, Action action)
        {
            _name = name;
            _description = description;
            _isUnlock = isUnlock;
            _isClaimed = isClaimed;
            _achievmentsReward = achievmentsReward;
            _eventBus.Subscribe < action.GetType() > (Unlock);
        }

        public void SetUnlock(bool isUnlock) 
        {
            _isUnlock = isUnlock;
        }

        public void Unlock()
        {
            _isUnlock = true;
            _achievmentsReward.GetReward();
        }

        public void Save(GameSaveData gameSaveData, LevelConfig levelConfig)
        {

        }

        public void Load(GameSaveData gameSaveData, LevelConfig levelConfig)
        {

        }

        //картинку сделать..

        //private Func<bool> _


    }
}
