using Assets._Scripts.EnteryPoints;
using Assets._Scripts.GameControllers;
using Assets._Scripts.ObjectsScripts.Coins;
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
        [SerializeField] private Transform _coins;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new GamePoints(_finishPoint, _checkPoints, _coins));

            builder.RegisterEntryPoint<CoinController>().AsSelf();
            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<LevelEnteryPoint>();
        }
    }
}
