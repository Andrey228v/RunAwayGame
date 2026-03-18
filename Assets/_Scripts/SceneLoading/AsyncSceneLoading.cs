using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneLoading
{
    public class AsyncSceneLoading
    {
        private readonly Dictionary<string, SceneInstance> _loaderScenes = new Dictionary<string, SceneInstance>();
        private CancellationTokenSource _cancellationTokenSource;
        private LoadScreenView _loadScreenView;

        public AsyncSceneLoading(LoadScreenView loadScreenView)
        {
            _loadScreenView = loadScreenView;
        }

        public async UniTask LoadScene(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _loadScreenView.Show();
            //await _loadScreenView.ShowAsync();
            await UniTask.Delay(TimeSpan.FromSeconds(2f), _cancellationTokenSource.IsCancellationRequested); // ДЛЯ ТЕСТА...
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            //SceneManager.SetActiveScene(loadedScene.Scene);
            //await _loadScreenView.HideAsync();
            _loadScreenView.Hide();

            //if (_loaderScenes.ContainsKey(sceneName) == false)
            //{
            //    _loaderScenes.Add(sceneName, loadedScene);
            //}



            _cancellationTokenSource.Cancel();
        }
    }
}
