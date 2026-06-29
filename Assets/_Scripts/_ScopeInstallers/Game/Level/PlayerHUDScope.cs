using Assets._Scripts.EnteryPoints;
using Assets._Scripts.ObjectsScripts.UI.GamePanel;
using Assets.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class PlayerHUDScope : LifetimeScope
    {
        [SerializeField] private GamePanelView _gamePanelControllerPrefab;

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
            builder.Register<GamePanelController>(Lifetime.Singleton);
            builder.Register<GamePanelModel>(Lifetime.Singleton);

            builder.RegisterFactory<GamePanelView>(container => () =>
            {
                return container.Instantiate(_gamePanelControllerPrefab);
            }, Lifetime.Transient);
        }
    }
}
