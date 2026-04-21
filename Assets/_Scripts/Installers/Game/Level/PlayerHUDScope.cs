using Assets._Scripts.EnteryPoints;
using Assets.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class PlayerHUDScope : LifetimeScope
    {
        [SerializeField] private GamePanelController _gamePanelControllerPrefab;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_gamePanelControllerPrefab == null)
            {
                Debug.LogError($"{_gamePanelControllerPrefab.name}: _gamePanelController is not set!", this);
            }
        }
#endif

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlayerHUDEnteryPoint>();

            builder.RegisterFactory<GamePanelController>(container => () =>
            {
                return container.Instantiate(_gamePanelControllerPrefab);
            }, Lifetime.Transient);
        }
    }
}
