using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;
        [SerializeField] private CameraController _cameraController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_character);
            builder.RegisterInstance(_animator);
            builder.RegisterInstance(_cameraController);
            builder.Register<PlayerMovement>(Lifetime.Singleton);
            builder.Register<PlayerRotator>(Lifetime.Singleton);
            builder.Register<Test>(Lifetime.Singleton);
            builder.Register<InputReader>(Lifetime.Singleton); // ????
            builder.Register<PlayerMoveDirectionCalculator>(Lifetime.Singleton);
            builder.Register<PlayerJumper>(Lifetime.Singleton);
            builder.Register<PlayerGroundChecker>(Lifetime.Singleton);
            builder.Register<PlayerGravityController>(Lifetime.Singleton);
            builder.Register<AnimatorController>(Lifetime.Singleton);
            builder.Register<FallController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<PlayerStateMachine>();
        }
    }
}
