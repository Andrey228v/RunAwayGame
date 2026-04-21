using Assets._Scripts.GameControllers.Achievments;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.UI._1MenuWindow.Achievements
{
    public class AchievementView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        [SerializeField] private float _hoverScale = 1.05f;
        [SerializeField] private float _duration = 0.2f;

        private AchievmentModel _achievmentModel;

        private bool _isUnlock;
        private bool _isSelected = false;
        private Vector3 _originalScale;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_name == null)
            {
                Debug.LogError($"{_name.name}: _name is not set!", this);
            }

            if (_descroption == null)
            {
                Debug.LogError($"{_descroption.name}: _descroption is not set!", this);
            }

            if (_claimButton == null)
            {
                Debug.LogError($"{_claimButton.name}: _claimButton is not set!", this);
            }
        }
#endif
        private void Start()
        {
            _originalScale = transform.localScale;
        }

        public void Construct(AchievmentModel achievmentModel)
        {
            if(achievmentModel == null)
            {
                throw new System.Exception("AchievementModel cannot be null. Please check the data source.");
            }

            _achievmentModel = achievmentModel;
            _name.text = achievmentModel.Name;
            _descroption.text = achievmentModel.Description;
            _isUnlock = achievmentModel.IsUnlock;
            
            if (_isUnlock)
            {
                _blockImage.gameObject.SetActive(false);

                if (_achievmentModel.CanClaim == false)
                {
                    _claimButton.gameObject.SetActive(false);
                }
                else
                {
                    _claimButton.gameObject.SetActive(true);
                }
            }
            else
            {
                _blockImage.gameObject.SetActive(true);
                _claimButton.gameObject.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(_hoverScale, _duration).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_originalScale, _duration).SetEase(Ease.OutQuad);
        }
    }
}
