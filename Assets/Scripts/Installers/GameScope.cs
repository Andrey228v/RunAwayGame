using Assets.Scripts.Camera;
using Assets.Scripts.EnteryPoints;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.UI;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CameraController _cameraController;

        [SerializeField] private GamePanelController _gamePanelController;
        [SerializeField] private StartPoint _startPoint;
        [SerializeField] private FinishPoint _finishPoint;

        [SerializeField] private Transform _checkPoints;

        [SerializeField] private LevelData _levelData;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_characterPrefab == null)
            {
                Debug.LogError($"{_characterPrefab.name}: _character is not set!", this);
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

            if (_checkPoints == null)
            {
                Debug.LogError($"{_checkPoints.name}: _checkPointers is not set!", this);
            }
        }
#endif

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_cameraController);
            builder.RegisterInstance(_gamePanelController);
            builder.RegisterInstance(new GamePoints(_startPoint, _finishPoint, _checkPoints));
            builder.RegisterInstance(_levelData);

            builder.Register<ISaveSystem, EasySaveSystem>(Lifetime.Singleton);
            builder.Register<PlayerData>(Lifetime.Singleton);
            builder.Register<SaveLoadService>(Lifetime.Singleton);
            builder.Register<PlayerStateMachineFactory>(Lifetime.Singleton);

            builder.RegisterFactory<Character>(container => () =>
            {
                return container.Instantiate(_characterPrefab);
            }, Lifetime.Transient);

            builder.RegisterEntryPoint<PlayerController>().AsSelf();
            builder.RegisterEntryPoint<GameEnteryPoint>();
        }
    }
}
