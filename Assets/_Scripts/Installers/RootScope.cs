using Assets._Scripts.EnteryPoints;
using Assets._Scripts.SceneLoading;
using Eflatun.SceneReference;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class RootScope : LifetimeScope
    {
        [SerializeField] private LoadScreenView _loadingScreenView;
        //[SerializeField] private SceneReference _loadingScene;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenView);
            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<AsyncSceneLoading>(Lifetime.Singleton);
        }
    }
}
