using Assets._Scripts.GameControllers.Achievments;
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
        [Header("UI Elements")]
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _descroption;
        [SerializeField] private Image _blockImage;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private Button _claimButton;
        [SerializeField] private GameObject _lockOverlay;

        [Header("Selection")]
        [SerializeField] private Image _selectionHighlight; // Обводка/подсветка
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _selectedColor = new Color(1, 0.8f, 0.2f);

        [Header("Animation")]
        [SerializeField] private float _animationDuration = 0.2f;

        private AchievmentModel _achievmentModel;

        private bool _isUnlock;
        private bool _isSelected = false;

        public void Construct(AchievmentModel achievmentModel)
        {
            _achievmentModel = achievmentModel;

            _name.text = achievmentModel.Name;
            _descroption.text = achievmentModel.Description;
            _isUnlock = achievmentModel.IsUnlock;
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
