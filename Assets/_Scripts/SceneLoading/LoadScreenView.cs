using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets._Scripts.SceneLoading
{
    public class LoadScreenView : MonoBehaviour
    {
        //[SerializeField] private SceneReference _loadingScene;    // Экран загрузки. Пока под вопросом так ли делать...
        //[SerializeField] private GameObject _screenRoot;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private Camera _loadingCamera;
        [SerializeField] private SceneGroup[] _sceneGroups;

        private float _targetProgress;
        private bool _isLoading;
        private float _fillSpeed = 0.5f;

        public readonly SceneGroupManager manager = new SceneGroupManager();

        private void Awake()
        {
            manager.OnSceneLoaded += sceneName => Debug.Log("Loaded: " + sceneName);
            manager.OnSceneUnloaded += sceneName => Debug.Log("Unloaded: " + sceneName);
            manager.OnSceneGroupLoaded += () => Debug.Log("On scene group loaded");
        }

        private void Update()
        {
            if (_isLoading == false) return;

            float currentFillAmount = _progressBar.value;
            float progressDif = Mathf.Abs(currentFillAmount - _targetProgress);
            float dynamicFillSpeed = progressDif * _fillSpeed;

            _progressBar.value = Mathf.Lerp(currentFillAmount, _targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        public async UniTask LoadSceneGroup(int index)
        {
            _progressBar.value = 0f;
            _targetProgress = 1f;

            if(index < 0 || index >= _sceneGroups.Length)
            {
                Debug.LogError("Invalid scene group index: " + index);
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.OnProgress += target => _targetProgress = Mathf.Max(target, _targetProgress); // Нет отписки...

            EnableLoadingCanvas();
            await manager.LoadScenes(_sceneGroups[index], progress);
            //await UniTask.Delay(1000);
            EnableLoadingCanvas(false);
        }

        public void EnableLoadingCanvas(bool enable = true)
        {
            _isLoading = enable;
            _loadingCanvas.gameObject.SetActive(enable);
            _loadingCamera.gameObject.SetActive(enable);
        }

        //public void Show()
        //{
        //    gameObject.SetActive(true);
        //    if (_canvasGroup != null)
        //        _canvasGroup.alpha = 1f;
        //}

        //public async UniTask ShowAsync()
        //{
        //    gameObject.SetActive(true);

        //    // Простая анимация появления (Fade in)
        //    if (_canvasGroup != null)
        //    {
        //        float elapsed = 0f;
        //        while (elapsed < 0.3f)
        //        {
        //            elapsed += Time.deltaTime;
        //            _canvasGroup.alpha = Mathf.Clamp01(elapsed / 0.3f);
        //            await UniTask.Yield(PlayerLoopTiming.Update);
        //        }
        //        _canvasGroup.alpha = 1f;
        //    }
        //}

        //public void UpdateProgress(float progress)
        //{
        //    if (_progressBar != null)
        //        _progressBar.value = Mathf.Clamp01(progress);
        //}

        //public void UpdateStatus(string status)
        //{
        //    if (_statusText != null)
        //        _statusText.text = status;
        //}

        //public void Hide()
        //{
        //    gameObject.SetActive(false);
        //}

        //public async UniTask HideAsync()
        //{
        //    // Простая анимация исчезновения (Fade out)
        //    if (_canvasGroup != null)
        //    {
        //        float elapsed = 0.3f;
        //        while (elapsed > 0f)
        //        {
        //            elapsed -= Time.deltaTime;
        //            _canvasGroup.alpha = Mathf.Clamp01(elapsed / 0.3f);
        //            await UniTask.Yield(PlayerLoopTiming.Update);
        //        }
        //        _canvasGroup.alpha = 0f;
        //    }

        //    gameObject.SetActive(false);
        //}
    }
}
