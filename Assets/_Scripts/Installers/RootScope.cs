using Assets._Scripts.EnteryPoints;
using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Service;
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
            builder.RegisterEntryPoint<BootEntryPoint>().AsSelf();
            builder.Register<EasySaveSystem>(Lifetime.Singleton);
            builder.Register<LoadManager>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameSaveLoadService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<LevelsController>().AsSelf();
            builder.RegisterEntryPoint<AchievmentsController>().AsSelf();
            builder.RegisterEntryPoint<ShopController>().AsSelf();
            builder.Register<WalletController>(Lifetime.Singleton).AsSelf();
            builder.Register<EventBus>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<IGameLogger>(container => new UnityLogger("Game"), Lifetime.Singleton);
        }
    }
}
