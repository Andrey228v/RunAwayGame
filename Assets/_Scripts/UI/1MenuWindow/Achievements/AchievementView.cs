using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Data;
using DG.Tweening;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _blockImage;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _currentCountText;
        [SerializeField] private TextMeshProUGUI _goalCountText;
        [SerializeField] private Button _claimButton;
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

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_name == null)
            {
                Debug.LogError($"{_name.name}: _name is not set!", this);
            }

            if (_description == null)
            {
                Debug.LogError($"{_description.name}: _descroption is not set!", this);
            }

            if (_claimButton == null)
            {
                Debug.LogError($"{_claimButton.name}: _claimButton is not set!", this);
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

        }

        public void ShowLocked()
        {
            _lockOverlay.SetActive(true);
            _claimButton.gameObject.SetActive(false);
            _blockImage.gameObject.SetActive(true);
            _progressBlock.SetActive(true);
        }

        public void ShowUnlocked(bool canClaim)
        {
            _lockOverlay.SetActive(false);
            _claimButton.gameObject.SetActive(canClaim);
            _blockImage.gameObject.SetActive(false);
            _progressBlock.SetActive(false);
        }

        public void ShowClaimed()
        {
            _claimButton.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(_hoverScale, _duration).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_originalScale, _duration).SetEase(Ease.OutQuad);
        }

        public void SetProgress(int current, int target)
        {

        }

        public void SetName(string name) => _name.text = name;
        public void SetDescription(string desc) => _description.text = desc;

        public void PlayUnlockAnimation()
        {
            transform.DOScale(1.2f, 0.3f).SetLoops(2, LoopType.Yoyo);
        }

        //public void UpdateUI(AchievmentData data, LevelConfig levelConfig)
        //{

        //}




        //public void Construct(IAchievement achievmentModel, IGameLogger gameLogger)
        //{
        //    if(achievmentModel == null)
        //    {
        //        throw new System.Exception("AchievementModel cannot be null. Please check the data source.");
        //    }

        //    //IAchievement _achievmentModel = achievmentModel;
        //    _name.text = achievmentModel.Data.Name;
        //    _descroption.text = achievmentModel.Data.Description;
        //    _isUnlock = achievmentModel.Data.IsUnlock;
        //    _gameLogger = gameLogger;


        //    if (_isUnlock)
        //    {
        //        _blockImage.gameObject.SetActive(false);

        //        if (achievmentModel.Data.IsClaimed == false)
        //        {
        //            _claimButton.gameObject.SetActive(false);
        //        }
        //        else
        //        {
        //            _claimButton.gameObject.SetActive(true);
        //        }
        //    }
        //    else
        //    {
        //        _blockImage.gameObject.SetActive(true);
        //        _claimButton.gameObject.SetActive(false);
        //    }
        //}



        //public void UpdateProgress()
        //{
        //    //if(_isUnlock) // Переделать...
        //        //Unlock();


        //}

        //public void Unlock()
        //{
        //    if(_isUnlock == false)
        //    {
        //        _gameLogger.Log($"AchievmentView Unlock", "Achievment");
        //        _isUnlock = true;
        //        _claimButton.gameObject.SetActive(true);
        //    }
        //}
    }
}
