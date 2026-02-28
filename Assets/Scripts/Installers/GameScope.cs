using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines;
using Assets.Scripts.StateMachines.Player;
using Assets.Scripts.StateMachines.Player.States;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Character _character;
        [SerializeField] private CameraController _cameraController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_character);
            builder.RegisterInstance(_cameraController);
            builder.Register<PlayerMovement>(Lifetime.Singleton);
            builder.Register<PlayerRotator>(Lifetime.Singleton);
            builder.Register<Test>(Lifetime.Singleton);
            builder.Register<InputReader>(Lifetime.Singleton); // ????
            builder.Register<PlayerMoveDirectionCalculator>(Lifetime.Singleton);
            builder.Register<PlayerStateMachine>(Lifetime.Singleton).As<ITickable, IFixedTickable>().AsSelf();
            builder.Register<PlayerJumper>(Lifetime.Singleton);
            builder.Register<PlayerGroundChecker>(Lifetime.Singleton);
            builder.Register<PlayerGravityController>(Lifetime.Singleton);
        }
    }
}
