using Assets._Scripts.EnteryPoints;
using Assets.Scripts.EnteryPoints;
using Assets.Scripts.Points;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class LevelScope : LifetimeScope
    {
        [SerializeField] private FinishPoint _finishPoint;
        [SerializeField] private Transform _checkPoints;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new GamePoints(_finishPoint, _checkPoints));

            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<LevelEnteryPoint>();
        }
    }
}
