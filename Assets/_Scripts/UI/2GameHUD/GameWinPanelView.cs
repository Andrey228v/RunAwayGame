using Assets._Scripts.ObjectsScripts.UI.GamePanel;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI._2GameHUD
{
    public class GameWinPanelView : MonoBehaviour, IPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _reloudButton;

        public event Action OnButtonBackToMenuClick;
        public event Action<WindowType> OnButtonBackToGameClick;

        public bool IsVisible { get; private set; }

        public string Name {  get;  set; } 

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (_backToMenuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToMenuButton is not set!", this);
            }

            if (_reloudButton == null)
            {
                Debug.LogError($"{gameObject.name}: _reloudButton is not set!", this);
            }
        }
#endif

        private void OnEnable()
        {
            _backToMenuButton.onClick.AddListener(ClickBackToMenu);
            _reloudButton.onClick.AddListener(ClickBackToGame);
        }

        private void OnDisable()
        {
            _backToMenuButton.onClick.RemoveListener(ClickBackToMenu);
            _reloudButton.onClick.RemoveListener(ClickBackToGame);
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

        private void ClickBackToMenu()
        {
            OnButtonBackToMenuClick?.Invoke();
        }

        private void ClickBackToGame()
        {
            OnButtonBackToGameClick?.Invoke(WindowType.InterfacePanel);
        }
    }
}
