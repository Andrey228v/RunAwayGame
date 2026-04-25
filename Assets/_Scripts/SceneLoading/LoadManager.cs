using Assets._Scripts.SaveLoad.Service;
using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneLoading
{
    public class LoadManager : IDisposable
    {
        //List который содержит все загруженные сцены.
        private Dictionary<string, AsyncOperationHandle> _loadsScenes; // ???
        private LoadScreenView _loadScreenView;
        private AsyncOperationGroup _asyncOperationGroup;
        private IProgress<float> _progress;
        private GameSaveLoadService _gameSaveLoadService;

        public LoadManager(LoadScreenView loadScreenView, GameSaveLoadService gameSaveLoadService)
        {
            _loadsScenes = new Dictionary<string, AsyncOperationHandle>();
            _loadScreenView = loadScreenView;
            _asyncOperationGroup = new AsyncOperationGroup();
            _progress = _loadScreenView.CreateProgressReporter();
            _gameSaveLoadService = gameSaveLoadService;
        }

        public void Dispose()
        {
            foreach (string key in _loadsScenes.Keys)
            {
                var handle = _loadsScenes[key];

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }

            _loadsScenes.Clear();
        }

        public async UniTask LoadScene(SceneGroupHandle sceneGroup)
        {
            ShowLoadScreen();
            await UnloadCurrentContent();

            await LoadNewContent(sceneGroup);
            PrepareToTransition(sceneGroup);

            _gameSaveLoadService.InitializeAllServices();
            _gameSaveLoadService.LoadAllServices();

            HideLoadScreen();
        }

        private void ShowLoadScreen()
        {
            _loadScreenView.SetProgress(0f);
            _loadScreenView.Show();
        }

        private async UniTask UnloadCurrentContent()
        {
            List<string> keys = _loadsScenes.Keys.ToList(); // Копия. Оказывается напрямую удалять нельзя ......
            _loadScreenView.SetStatus("Выгрузка");

            foreach (string key in keys)
            {
                var handle = _loadsScenes[key];

                await Addressables.UnloadSceneAsync(handle).ToUniTask(_progress);

                _loadsScenes.Remove(key);
            }

            _asyncOperationGroup.ReleaseAll();
            await Resources.UnloadUnusedAssets();
        }

        private async UniTask LoadNewContent(SceneGroupHandle sceneGroup)
        {

            foreach (SceneWrapper sceneWrapper in sceneGroup.scenesNames)
            {
                SceneReference scene = sceneWrapper.scene;

                await UniTask.Delay(100);
                AsyncOperationHandle handle = Addressables.LoadSceneAsync(scene.Address, LoadSceneMode.Additive);

                _loadsScenes.Add(sceneWrapper.Name, handle);
                _asyncOperationGroup.Add(handle);

                _loadScreenView.SetStatus($"Загрузка сцены: {sceneWrapper.Name}");
            }

            await _asyncOperationGroup.WhenAll(_progress);

            Debug.Log("1)_____WhenAll_______");
            Debug.Log("2)_____WAIT_UNTIL_______");
        }

        private void PrepareToTransition(SceneGroupHandle sceneGroup)
        {
            _loadScreenView.SetStatus($"Переход");
            string activeSceneName = sceneGroup.FindSceneNameByType(SceneType.Active);

            SceneReference sceneRef = sceneGroup.FindScene(activeSceneName).scene;
            Scene scene = SceneManager.GetSceneByName(sceneRef.Name);

            SceneManager.SetActiveScene(scene);

            Debug.Log("3)_____SetActiveScene_______");
        }

        private void HideLoadScreen()
        {
            _loadScreenView.Hide();

            Debug.Log("4)_____HideLoadScreen_______");
        }
    }
}
