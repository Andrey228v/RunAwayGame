using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI._1MenuWindow.Achievements
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _descroption;
        [SerializeField] private Image _blockImage;

        private bool _isUnlock;
        
        public void Construct(string name, string description, bool isUnlock)
        {
            _name.text = name;
            _descroption.text = description;
            _isUnlock = isUnlock;
        }

        private void Start()
        {
            if (_isUnlock)
            {
                _blockImage.gameObject.SetActive(false);
            }
        }
    }
}
