using Assets._Scripts.SceneLoading;
using Assets._Scripts.Utilites;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class BootEntryPoint : IInitializable
    {
        private LoadScreenView _screenView;
        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;

        public BootEntryPoint(LoadManager loadManager, List<SceneGroupHandle> scensGroups)
        {
            //_screenView = loadScreenView;
            _loadManager = loadManager;
            _scensGroups = scensGroups;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);

            await _loadManager.LoadScene(_scensGroups[0]);
        }
    }
}
