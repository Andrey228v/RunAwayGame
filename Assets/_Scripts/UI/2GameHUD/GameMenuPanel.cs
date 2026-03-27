using Assets._Scripts.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameMenuPanel : MonoBehaviour, IPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button _backToGameButton;
        [SerializeField] private Button _backToMenuButton;

        public event Action<string> OnBackToGameButtonClick;
        public event Action OnBackToMenuButtonClick;

        public bool IsVisible { get; private set; }

        public string Name { get; set; }

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (_backToGameButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToGameButton is not set!", this);
            }

            if (_backToMenuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToMenuButton is not set!", this);
            }
        }
#endif

        private void OnEnable()
        {
            _backToGameButton.onClick.AddListener(ClickBackToGame);
            _backToMenuButton.onClick.AddListener(ClickBackToMenu);
        }

        private void OnDisable()
        {
            _backToGameButton.onClick.RemoveListener(ClickBackToGame);
            _backToMenuButton.onClick.RemoveListener(ClickBackToMenu);
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

        private void ClickBackToGame()
        {
            OnBackToGameButtonClick?.Invoke("GameInterfacePanel"); // переделать...
        }

        private void ClickBackToMenu()
        {
            OnBackToMenuButtonClick?.Invoke();
        }
    }
}
