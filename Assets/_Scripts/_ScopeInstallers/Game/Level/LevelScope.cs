using Assets._Scripts.EnteryPoints;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets.Scripts.Points;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class LevelScope : LifetimeScope
    {
        [SerializeField] private LevelConfig _levelConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoinController>().AsSelf();
            builder.RegisterEntryPoint<CoinDictinaryModel>().AsSelf();
            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<FinishController>().AsSelf();
            builder.RegisterEntryPoint<FinishModel>().AsSelf();
            builder.RegisterEntryPoint<LevelEnteryPoint>();
            builder.RegisterInstance(_levelConfig);
        }
    }
}
