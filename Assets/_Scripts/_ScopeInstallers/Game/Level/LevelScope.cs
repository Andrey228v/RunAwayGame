using Assets._Scripts.EnteryPoints;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets.Scripts.Points;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class LevelScope : LifetimeScope
    {
        [SerializeField] private LevelConfig _levelConfigs;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoinController>().AsSelf();
            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<LevelEnteryPoint>();
        }
    }
}
