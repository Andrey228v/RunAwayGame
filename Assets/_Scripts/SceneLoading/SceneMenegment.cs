using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using System;
using System.Collections.Generic;
using System.Linq;
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;
//using UnityEngine.ResourceManagement.ResourceProviders;
//using UnityEngine.SceneManagement;
//using static Assets._Scripts.SceneLoading.SceneGroup;

namespace Assets._Scripts.SceneLoading
{
    public class SceneGroupManager
    {
        //    public event Action<string> OnSceneLoaded;
        //    public event Action<string> OnSceneUnloaded;
        //    public event Action OnSceneGroupLoaded;

        //    public SceneGroup activeSceneGroup;

        //    public readonly AsyncOperationHandleGroup handleGroup = new AsyncOperationHandleGroup(10);

        //    public async UniTask LoadScenes(SceneGroup sceneGroup, IProgress<float> progress, bool reloudDupScenes  = false) 
        //    {
        //        activeSceneGroup = sceneGroup;

        //        var loadedScenes = new List<string>();

        //        await UnloadScenes();

        //        int sceneCount = SceneManager.sceneCount;

        //        for (int i = 0; i < sceneCount; i++)
        //        {
        //            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        //        }

        //        var totalSceneLoaded = activeSceneGroup.scenes.Count;

        //        var operationGroup = new AsyncOperationGroup(totalSceneLoaded);

        //        for (int i = 0; i < totalSceneLoaded; i++)
        //        {
        //            var sceneData = sceneGroup.scenes[i];

        //            if (reloudDupScenes == false && loadedScenes.Contains(sceneData.name)) 
        //            {
        //                continue;
        //            }

        //            //Заглушка... пересмотреть..
        //            if(sceneData.reference.State == SceneReferenceState.Regular)
        //            {
        //                var operation = SceneManager.LoadSceneAsync(sceneData.reference.Path, LoadSceneMode.Additive);
        //                operationGroup.operations.Add(operation);
        //            }
        //            else if (sceneData.reference.State == SceneReferenceState.Addressable)
        //            {
        //                var operation = Addressables.LoadSceneAsync(sceneData.reference.Name, LoadSceneMode.Additive);
        //                handleGroup.handles.Add(operation);
        //            }


        //                OnSceneLoaded?.Invoke(sceneData.name);
        //        }

        //        while(operationGroup.IsCompleted == false || handleGroup.IsCompleted == false)
        //        {
        //            progress?.Report((operationGroup.Progress + handleGroup.progress)/2); // заглушка...
        //            await UniTask.Delay(100);
        //        }

        //        //Тоже пересмотреть....
        //        Scene activeScene = SceneManager.GetSceneByName(activeSceneGroup.FindSceneNameByType(SceneType.ActiveScene));

        //        if (activeScene.IsValid())
        //        {
        //            SceneManager.SetActiveScene(activeScene);
        //        }

        //        OnSceneGroupLoaded?.Invoke();

        //    }

        //    public async UniTask UnloadScenes() 
        //    {
        //        var scenes = new List<string>(); 
        //        var activeScene = SceneManager.GetActiveScene().name;
        //        int sceneCount = SceneManager.sceneCount;

        //        for(int i = sceneCount - 1; i > 0; i--)
        //        {
        //            var sceneAt = SceneManager.GetSceneAt(i);

        //            if(sceneAt.isLoaded == false)
        //            {
        //                continue;
        //            }

        //            var sceneName = sceneAt.name;

        //            if(sceneName.Equals(activeScene) || sceneName == "Boot")
        //            {
        //                continue;
        //            }

        //            if (handleGroup.handles.Any(h => h.IsValid() && h.Result.Scene.name == sceneName)) continue;

        //            scenes.Add(sceneName);
        //        }

        //        var operationGroup = new AsyncOperationGroup(scenes.Count);

        //        foreach(var scene in scenes)
        //        {
        //            var operation = SceneManager.UnloadSceneAsync(scene);

        //            if(operation == null)
        //            {
        //                continue;
        //            }
        //            operationGroup.operations.Add(operation);

        //            OnSceneUnloaded?.Invoke(scene);
        //        }

        //        foreach(var handle in handleGroup.handles)
        //        {
        //            if (handle.IsValid())
        //            {
        //                Addressables.UnloadSceneAsync(handle);
        //            }
        //        }

        //        handleGroup.handles.Clear();

        //        while(operationGroup.IsCompleted == false)
        //        {
        //            await UniTask.Delay(100);
        //        }


        //        //ТУТ НАДО ТЕСТИРОВАТЬ С АДРЕСАББЛЕС....
        //        await Resources.UnloadUnusedAssets();
        //    }
        //}

        //public readonly struct AsyncOperationGroup
        //{
        //    public readonly List<AsyncOperation> operations;

        //    public float Progress => operations.Count == 0? 0: operations.Average(o => o.progress);
        //    public bool IsCompleted => operations.All(o => o.isDone);

        //    public AsyncOperationGroup(int initCapacity)
        //    {
        //        operations = new List<AsyncOperation>();
        //    }
        //}

        //public readonly struct AsyncOperationHandleGroup
        //{
        //    public readonly List<AsyncOperationHandle<SceneInstance>> handles;
        //    public float progress => handles.Count == 0? 0: handles.Average(h => h.PercentComplete);
        //    public bool IsCompleted => handles.Count == 0 || handles.All(o => o.IsDone);

        //    public AsyncOperationHandleGroup(int initialCapacity)
        //    {
        //        handles = new List<AsyncOperationHandle<SceneInstance>>(initialCapacity);
        //    }
        //}

        //public class LoadingProgress : IProgress<float>
        //{
        //    public event Action<float> OnProgress;

        //    const float ratio = 1f;

        //    public void Report(float value)
        //    {
        //        OnProgress?.Invoke(value / ratio);
        //    }

        //}


        //[Serializable]
        //public class SceneGroup
        //{
        //    public string groupName = "New Scene Group";
        //    public List<SceneData> scenes;

        //    public string FindSceneNameByType(SceneType sceneType)
        //    {
        //        return scenes.FirstOrDefault(scene => scene.sceneType == sceneType)?.name;
        //    }

        //    public SceneData FindScene(string sceneName)
        //    {
        //        return scenes.FirstOrDefault(scene => scene.name == sceneName);
        //    }


        //    [Serializable]
        //    public class SceneData
        //    {
        //        public SceneReference reference;
        //        public string name => reference.Name;
        //        public SceneType sceneType;
        //    }

        //    public enum SceneType { ActiveScene, MainMenu, HUD, Cinematic, Enviroment, Player }
        //}
    }
}
