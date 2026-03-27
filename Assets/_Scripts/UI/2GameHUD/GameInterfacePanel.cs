using Assets._Scripts.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameInterfacePanel : MonoBehaviour, IPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _saveButton;

        [Header("Game data")]
        [SerializeField] private TextMeshProUGUI _coinsCounter;
        [SerializeField] private TextMeshProUGUI _timer;

        public event Action<string> OnMenuButtonClick;
        public event Action OnLoadButtonClick;
        public event Action OnSoundButtonClick;
        public event Action OnSaveButtonClick;

        public bool IsVisible { get; private set; }

        public string Name { get; set; }

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (_menuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _menuButton is not set!", this);
            }

            if (_loadButton == null)
            {
                Debug.LogError($"{gameObject.name}: _loadButton is not set!", this);
            }

            if (_soundButton == null)
            {
                Debug.LogError($"{gameObject.name}: _soundButton is not set!", this);
            }

            if (_coinsCounter == null)
            {
                Debug.LogError($"{gameObject.name}: _coinsCounter is not set!", this);
            }

            if (_timer == null)
            {
                Debug.LogError($"{gameObject.name}: _timer is not set!", this);
            }
        }
#endif

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(ClickMenuButton);
            _loadButton.onClick.AddListener(ClickLoadButton);
            _soundButton.onClick.AddListener(ClickSoundButton);
            _saveButton.onClick.AddListener(ClickSaveButton);

        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(ClickMenuButton);
            _loadButton.onClick.RemoveListener(ClickLoadButton);
            _soundButton.onClick.RemoveListener(ClickSoundButton);
            _saveButton.onClick.RemoveListener(ClickSaveButton);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }

        private void ClickMenuButton()
        {
            OnMenuButtonClick?.Invoke("GameMenuPanel"); // переделать...
        }

        private void ClickLoadButton()
        {
            OnLoadButtonClick?.Invoke();
        }

        private void ClickSoundButton()
        {
            OnSoundButtonClick?.Invoke();
        }

        private void ClickSaveButton()
        {
            Debug.Log("SAVE");
            OnSaveButtonClick?.Invoke();
        }
    }
}

