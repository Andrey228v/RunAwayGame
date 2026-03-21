using Assets._Scripts.EnteryPoints;
using Assets._Scripts.SceneLoading;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Service;
using Eflatun.SceneReference;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class RootScope : LifetimeScope
    {
        [SerializeField] private LoadScreenView _loadingScreenView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenView);
            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<ISaveSystem, EasySaveSystem>(Lifetime.Singleton);
            builder.Register<SaveLoadService>(Lifetime.Singleton);
            builder.Register<ISaveService, SaveLoadService>(Lifetime.Singleton);


            builder.RegisterFactory<LoadScreenView>(container => () =>
            {
                return container.Instantiate(_loadingScreenView);
            }, Lifetime.Singleton);
        }
    }
}
