using Assets.Scripts.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private CharacterController _characterController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_characterController);
            builder.Register<PlayerMovement>(Lifetime.Singleton);
            builder.Register<Test>(Lifetime.Singleton);
        }
    }
}
