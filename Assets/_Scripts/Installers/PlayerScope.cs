using Assets._Scripts.EnteryPoints;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class PlayerScope : LifetimeScope
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CameraController _cameraController;


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
        }
#endif

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_cameraController);
            builder.RegisterEntryPoint<PlayerController>().AsSelf();
            builder.RegisterEntryPoint<PlayerEnteryPoint>();
            builder.Register<PlayerStateMachineFactory>(Lifetime.Singleton);


            builder.RegisterFactory<Character>(container => () =>
            {
                return container.Instantiate(_characterPrefab);
            }, Lifetime.Transient);


        }
    }
}
