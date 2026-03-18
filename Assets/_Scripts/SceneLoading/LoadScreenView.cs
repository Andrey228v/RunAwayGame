using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets._Scripts.SceneLoading
{
    public class LoadScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject _screenRoot;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private CanvasGroup _canvasGroup;


        public void Show()
        {
            _screenRoot.SetActive(true);
            if (_canvasGroup != null)
                _canvasGroup.alpha = 1f;
        }

        public async UniTask ShowAsync()
        {
            _screenRoot.SetActive(true);

            // Простая анимация появления (Fade in)
            if (_canvasGroup != null)
            {
                float elapsed = 0f;
                while (elapsed < 0.3f)
                {
                    elapsed += Time.deltaTime;
                    _canvasGroup.alpha = Mathf.Clamp01(elapsed / 0.3f);
                    await UniTask.Yield(PlayerLoopTiming.Update);
                }
                _canvasGroup.alpha = 1f;
            }
        }

        public void UpdateProgress(float progress)
        {
            if (_progressBar != null)
                _progressBar.value = Mathf.Clamp01(progress);
        }

        public void UpdateStatus(string status)
        {
            if (_statusText != null)
                _statusText.text = status;
        }

        public void Hide()
        {
            _screenRoot.SetActive(false);
        }

        public async UniTask HideAsync()
        {
            // Простая анимация исчезновения (Fade out)
            if (_canvasGroup != null)
            {
                float elapsed = 0.3f;
                while (elapsed > 0f)
                {
                    elapsed -= Time.deltaTime;
                    _canvasGroup.alpha = Mathf.Clamp01(elapsed / 0.3f);
                    await UniTask.Yield(PlayerLoopTiming.Update);
                }
                _canvasGroup.alpha = 0f;
            }

            _screenRoot.SetActive(false);
        }
    }
}
