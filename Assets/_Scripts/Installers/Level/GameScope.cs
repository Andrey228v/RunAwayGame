using Assets._Scripts.GameControllers;
using Assets.Scripts.EnteryPoints;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEnteryPoint>();

            builder.Register<GameFinishController>(Lifetime.Singleton); // под вопросом...
            builder.Register<GameRestartController>(Lifetime.Singleton); // под вопросом...
            builder.Register<GameManager>(Lifetime.Singleton);
        }
    }
}
