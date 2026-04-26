using Assets._Scripts.EnteryPoints;
using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
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

        //[SerializeField] private GameSaveData _gameSaveData;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelConfigs);
            builder.RegisterInstance(_sceneGroupHandle);
            builder.RegisterInstance(_loadScreenView);
            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<EasySaveSystem>(Lifetime.Singleton);
            builder.Register<LoadManager>(Lifetime.Singleton);
            builder.Register<GameManager>(Lifetime.Singleton);
            builder.Register<GameSaveLoadService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<LevelsController>().AsSelf();
            builder.RegisterEntryPoint<AchievmentsController>().AsSelf();
            builder.RegisterEntryPoint<ShopController>().AsSelf();
        }
    }
}
