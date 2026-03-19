using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
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
        private SceneReference _loadingScene;

        public AsyncSceneLoading(SceneReference loadingScene)
        {
            _loadingScene = loadingScene;
        }

        public async UniTask LoadScene(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            //_loadScreenView.Show();
            //await _loadScreenView.ShowAsync();    
            await SceneManager.LoadSceneAsync(_loadingScene.Name, LoadSceneMode.Additive);

            await UniTask.Delay(TimeSpan.FromSeconds(2f), _cancellationTokenSource.IsCancellationRequested); // ДЛЯ ТЕСТА...
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            //SceneManager.SetActiveScene(loadedScene.Scene);
            //await _loadScreenView.HideAsync();
            //_loadScreenView.Hide();

            //if (_loaderScenes.ContainsKey(sceneName) == false)
            //{
            //    _loaderScenes.Add(sceneName, loadedScene);
            //}

            await SceneManager.UnloadSceneAsync(_loadingScene.Name);

            _cancellationTokenSource.Cancel();
        }
    }
}
