using Assets._Scripts.EnteryPoints;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets.Scripts.Points;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoinController>().AsSelf();
            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<LevelEnteryPoint>();
        }
    }
}
