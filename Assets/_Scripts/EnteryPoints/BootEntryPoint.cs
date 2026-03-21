using Assets._Scripts.SceneLoading;
using Assets._Scripts.Utilites;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class BootEntryPoint : IInitializable
    {
        private LoadScreenView _screenView;
        private Func<LoadScreenView> _loadScreenFactory;

        public BootEntryPoint(Func<LoadScreenView> loadScreenFactory)
        {
            _loadScreenFactory = loadScreenFactory;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);

            _screenView = _loadScreenFactory();

            await UniTask.Delay(100);
            await _screenView.LoadSceneGroup(0);
        }
    }
}
