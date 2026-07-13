using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.UI._1MenuWindow.Achievements
{
    public class AchievementView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI Elements")]
        //[SerializeField] private Image _icon;
        [SerializeField] private string _id;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _blockImage;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _currentCountText;
        [SerializeField] private TextMeshProUGUI _goalCountText;
        [SerializeField] private Button _takeRewardButton;
        [SerializeField] private GameObject _lockOverlay;
        [SerializeField] private GameObject _progressBlock;

        [Header("Selection")]
        [SerializeField] private Image _selectionHighlight; // Обводка/подсветка
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _selectedColor = new Color(1, 0.8f, 0.2f);

        [Header("Animation")]
        [SerializeField] private float _hoverScale = 1.05f;
        [SerializeField] private float _duration = 0.2f;

        private Vector3 _originalScale;
        private IGameLogger _gameLogger;

        //public Button TakeRewardButton => _takeRewardButton;

        public string Id => _id;

        public event Action OnTakeRewardButtonClick;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_id == null)
            {
                Debug.LogError($"{_id}: _id is not set!", this);
            }

            if (_name == null)
            {
                Debug.LogError($"{_name.name}: _name is not set!", this);
            }

            if (_description == null)
            {
                Debug.LogError($"{_description.name}: _descroption is not set!", this);
            }

            if (_takeRewardButton == null)
            {
                Debug.LogError($"{_takeRewardButton.name}: _claimButton is not set!", this);
            }

            if(_progressBlock == null)
            {
                Debug.LogError($"{_progressBlock.name}: _progressBlock is not set!", this);
            }
        }
#endif

        [Inject]
        public void Construct(IGameLogger gameLogger)
        {
            _gameLogger = gameLogger;
        }

        private void Start()
        {
            _originalScale = transform.localScale;

            _takeRewardButton.onClick.AddListener(TakeRewardButtonClick);
        }

        private void OnDestroy()
        {
            transform.DOKill();
            _takeRewardButton.onClick.RemoveListener(TakeRewardButtonClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(_hoverScale, _duration).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_originalScale, _duration).SetEase(Ease.OutQuad);
        }

        public void SetDataView(AchievmentData data)
        {
            if (data.IsUnlock && data.IsRevardEnable)
            {
                ShowUnlockedWithButtonReward();
            }
            else if (data.IsUnlock == false)
            {
                ShowLocked();
            }
            else if (data.IsUnlock && data.IsUnlockAndTaken)
            {
                ShowUnlokedAfterReward();
            }

            _progressBar.value = Mathf.Clamp01(data.Progress);
            _currentCountText.text = data.CurrentValue.ToString();
            _goalCountText.text = data.TargetValue.ToString();
        }

        public void SetName(string name) => _name.text = name;

        public void SetDescription(string desc) => _description.text = desc;

        private void ShowLocked()
        {
            _lockOverlay.SetActive(true);
            _takeRewardButton.gameObject.SetActive(false);
            _blockImage.gameObject.SetActive(true);
            _progressBlock.SetActive(true);
            _progressBar.gameObject.SetActive(true);
        }

        private void ShowUnlockedWithButtonReward()
        {
            _lockOverlay.SetActive(false);
            _takeRewardButton.gameObject.SetActive(true);
            _blockImage.gameObject.SetActive(false);
            _progressBlock.SetActive(false);
            _progressBar.gameObject.SetActive(false);
        }

        private void ShowUnlokedAfterReward()
        {
            _lockOverlay.SetActive(false);
            _takeRewardButton.gameObject.SetActive(false);
            _blockImage.gameObject.SetActive(false);
            _progressBlock.SetActive(false);
            _progressBar.gameObject.SetActive(false);
        }

        private void TakeRewardButtonClick()
        {
            OnTakeRewardButtonClick?.Invoke();
        }
    }
}
