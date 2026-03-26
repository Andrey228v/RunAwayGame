using Assets.Scripts.Camera;
using Assets.Scripts.EnteryPoints;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private FinishPoint _finishPoint;
        [SerializeField] private Transform _checkPoints;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new GamePoints(_finishPoint, _checkPoints));
            builder.Register<PlayerData>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<GameEnteryPoint>();
        }
    }
}
