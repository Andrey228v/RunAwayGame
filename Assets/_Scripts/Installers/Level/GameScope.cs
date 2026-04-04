using Assets._Scripts.GameControllers;
using Assets._Scripts.UI;
using Assets.Scripts.Camera;
using Assets.Scripts.EnteryPoints;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private UnitInfoUI _unitInfoUIPrefab;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_unitInfoUIPrefab == null)
            {
                Debug.LogError($"{_unitInfoUIPrefab.name}: _unitInfoUIPrefab is not set!", this);
            }
        }
#endif

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEnteryPoint>();

            builder.Register<GameFinishController>(Lifetime.Singleton); // под вопросом...
            builder.Register<GameRestartController>(Lifetime.Singleton); // под вопросом...
            builder.Register<GameManager>(Lifetime.Singleton);
            builder.RegisterEntryPoint<BillboardManager>().AsSelf();

            builder.RegisterFactory<UnitInfoUI>(container => () =>
            {
                return container.Instantiate(_unitInfoUIPrefab);
            }, Lifetime.Transient);
        }
    }
}
