using Assets._Scripts.SceneLoading;
using Assets._Scripts.Utilites;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class BootEntryPoint : IInitializable
    {
        private LoadScreenView _screenView;

        public BootEntryPoint(LoadScreenView loadScreenView)
        {
            _screenView = loadScreenView;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);

            await UniTask.Delay(100);
            await _screenView.LoadSceneGroup(0);
        }
    }
}
