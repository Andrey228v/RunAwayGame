using Assets._Scripts.EnteryPoints;
using Assets._Scripts.SceneLoading;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class RootScope : LifetimeScope
    {
        [SerializeField] private List<LevelConfig> _levelConfigs;
        [SerializeField] private List<SceneGroupHandle> _sceneGroupHandle;
        [SerializeField] private LoadScreenView _loadScreenView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelConfigs);
            builder.RegisterInstance(_sceneGroupHandle);
            builder.RegisterInstance(_loadScreenView);
            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<EasySaveSystem>(Lifetime.Singleton);
            builder.Register<SaveLoadService>(Lifetime.Singleton);
            builder.Register<LoadManager>(Lifetime.Singleton);
        }
    }
}
