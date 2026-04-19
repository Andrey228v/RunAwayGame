using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentModel
    {
        private string _name;
        private string _description;
        private bool _isUnlock;

        public string Name => _name;
        public string Description => _description;
        public bool IsUnlock => _isUnlock;

        public AchievmentModel(string name, string description, bool isUnlock)
        {
            _name = name;
            _description = description;
            _isUnlock = isUnlock;
        }

        public void SetUnlock(bool isUnlock) 
        {
            _isUnlock = isUnlock;
        }

        //картинку сделать..

        //private Func<bool> _


    }
}
