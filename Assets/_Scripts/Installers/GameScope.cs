using Assets._Scripts.GameControllers;
using Assets.Scripts.EnteryPoints;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private FinishPoint _finishPoint;
        [SerializeField] private Transform _checkPoints;

        //private List<IRestart> _restartList;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new GamePoints(_finishPoint, _checkPoints));
            builder.Register<PlayerData>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<GameEnteryPoint>();

            builder.Register<GameCycleController>(Lifetime.Singleton); // под вопросом...
        }
    }
}
