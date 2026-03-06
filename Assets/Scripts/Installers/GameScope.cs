using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.StateMachines.Player;
using Assets.Scripts.UI;
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

        [SerializeField] private GamePanelController _gamePanelController;
        [SerializeField] private StartPoint _startPoint;
        [SerializeField] private FinishPoint _finishPoint;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_character == null)
            {
                Debug.LogError($"{_character.name}: _character is not set!", this);
            }

            if (_animator == null)
            {
                Debug.LogError($"{_animator.name}: _animator is not set!", this);
            }

            if (_cameraController == null)
            {
                Debug.LogError($"{_cameraController.name}: _cameraController is not set!", this);
            }

            if (_gamePanelController == null)
            {
                Debug.LogError($"{_gamePanelController.name}: _gamePanelController is not set!", this);
            }

            if (_startPoint == null)
            {
                Debug.LogError($"{_startPoint.name}: _spawnPoint is not set!", this);
            }
        }
#endif


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_character);
            builder.RegisterInstance(_animator);
            builder.RegisterInstance(_startPoint);
            builder.RegisterInstance(_finishPoint);
            builder.RegisterInstance(_cameraController);
            builder.RegisterInstance(_gamePanelController);
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
            builder.Register<ISaveLoad, PlayerController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<PlayerStateMachine>();

            //Под вопросом...
            builder.Register<ISaveSystem, EasySaveSystem>(Lifetime.Singleton);
            builder.Register<PlayerData>(Lifetime.Singleton);

            //SL
            builder.Register<ISaveLoad, TestSL1>(Lifetime.Singleton);
            builder.Register<SaveLoadService>(Lifetime.Singleton);
            //builder.Register<IInitializable, SaveLoadService>(Lifetime.Singleton);

        }
    }
}
