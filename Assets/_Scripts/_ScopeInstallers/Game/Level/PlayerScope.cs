using Assets._Scripts.EnteryPoints;
using Assets._Scripts.ObjectsScripts.Camera;
using Assets._Scripts.ObjectsScripts.Player;
using Assets._Scripts.ObjectsScripts.Player.Factorys;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class PlayerScope : LifetimeScope
    {
        
        [SerializeField] private CameraController _cameraController;

#if UNITY_EDITOR
        private void OnValidate()
        {
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
        }
    }
}
