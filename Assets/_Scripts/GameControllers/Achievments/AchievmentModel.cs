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

        public float Progress => (float)_currentValue / _targetValue;
        public string Name => _name;
        public string Description => _description;
        public bool IsUnlock => _isUnlock;
        public bool CanClaim => _isUnlock && !_isClaimed;


        public AchievmentModel(string name, string description, bool isUnlock, bool isClaimed)
        {
            _name = name;
            _description = description;
            _isUnlock = isUnlock;
            _isClaimed = isClaimed;
        }

        public void SetUnlock(bool isUnlock) 
        {
            _isUnlock = isUnlock;
        }

        //картинку сделать..

        //private Func<bool> _


    }
}
