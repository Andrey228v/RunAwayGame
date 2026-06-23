using Assets._Scripts.EnteryPoints.Level;
using Assets._Scripts.ObjectsScripts.Bots;
using Assets._Scripts.ObjectsScripts.Bots.Factorys;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers.Level
{
    public class BotAIScope : LifetimeScope
    {
        [SerializeField] private Transform _botsParent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new BotTransformList(_botsParent));

            builder.Register<BotFactory>(Lifetime.Singleton);
            builder.Register<BotsController>(Lifetime.Singleton).As<IFixedTickable, IStartable>().AsSelf();
            builder.Register<BotStateMachineFactory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BotAIEnteryPoint>();
        }
    }
}
