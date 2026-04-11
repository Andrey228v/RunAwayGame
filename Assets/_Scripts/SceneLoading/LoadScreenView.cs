using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SceneLoading
{
    public class LoadScreenView : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private Camera _loadingCamera;

        public void Show()
        {
            _loadingCanvas.gameObject.SetActive(true);
            _loadingCamera.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _loadingCanvas.gameObject.SetActive(false);
            _loadingCamera.gameObject.SetActive(false);
        }

        public void SetProgress(float progress)
        {
            _progressBar.value = progress;
        }

        public void SetStatus(string status)
        {
            _statusText.text = status;
        }

        public IProgress<float> CreateProgressReporter()
        {
            return Progress.Create<float>(value =>
            {
                _progressBar.value = value;
            });
        }
    }
}
