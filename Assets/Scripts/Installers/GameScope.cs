using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CameraController _cameraController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_characterController);
            builder.RegisterInstance(_cameraController);
            builder.Register<PlayerMovement>(Lifetime.Singleton).As<ITickable>().AsSelf();
            builder.Register<PlayerRotator>(Lifetime.Singleton).As<ITickable>().AsSelf();
            builder.Register<Test>(Lifetime.Singleton);
            builder.Register<InputReader>(Lifetime.Singleton); // ????
            builder.Register<PlayerMoveDirectionCalculator>(Lifetime.Singleton);
        }
    }
}
