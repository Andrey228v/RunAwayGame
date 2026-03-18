using Assets._Scripts.SceneLoading;
using Assets._Scripts.Utilites;
using DG.Tweening;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class BootEntryPoint : IInitializable
    {
        private AsyncSceneLoading _sceneLoading;

        public BootEntryPoint(AsyncSceneLoading sceneLoading)
        {
            _sceneLoading = sceneLoading;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);
            await _sceneLoading.LoadScene(Scenes.MENU);
        }
    }
}
