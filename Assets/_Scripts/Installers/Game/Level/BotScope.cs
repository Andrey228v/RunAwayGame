using Assets._Scripts.Bots;
using Assets._Scripts.Bots.Factorys;
using Assets._Scripts.EnteryPoints.Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers.Level
{
    public class BotScope : LifetimeScope
    {
        [SerializeField] private Transform _botsParent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new BotTransformList(_botsParent));

            builder.Register<BotFactory>(Lifetime.Singleton);
            builder.Register<BotsController>(Lifetime.Singleton).As<IFixedTickable>().AsSelf();
            builder.Register<BotStateMachineFactory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BotEnteryPoint>();

        }
    }
}
