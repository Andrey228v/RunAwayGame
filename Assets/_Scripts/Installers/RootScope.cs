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
        [SerializeField] private LoadScreenView _loadingScreenView;
        [SerializeField] private List<LevelConfig> _levelConfigs;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelConfigs);
            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<ISaveSystem, EasySaveSystem>(Lifetime.Singleton);
            builder.Register<SaveLoadService>(Lifetime.Singleton);
            //builder.Register<ISaveService, SaveLoadService>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_loadingScreenView, Lifetime.Singleton);
            
        }
    }
}
