using Assets._Scripts.GameControllers;
using Assets._Scripts.UI;
using Assets.Scripts.EnteryPoints;
using Assets.Scripts.Points;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private UnitInfoUI _unitInfoUIPrefab;
        [SerializeField] private Character _characterPrefab; // префаб Unit, тут надо префаб делать пустой без Player и Bot. Потом переделать.
        [SerializeField] private NavMeshCharacter _navMeshCharacterPrefab;

        [SerializeField] private FinishPoint _finishPoint; // Переделать
        [SerializeField] private Transform _checkPoints; // Переделать
        [SerializeField] private Transform _coins; // Переделать
        [SerializeField] private Transform _botsRoad; // Переделать

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_unitInfoUIPrefab == null)
            {
                Debug.LogError($"{_unitInfoUIPrefab.name}: _unitInfoUIPrefab is not set!", this);
            }

            if (_characterPrefab == null)
            {
                Debug.LogError($"{_characterPrefab.name}: _character is not set!", this);
            }
        }
#endif

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEnteryPoint>();

            builder.RegisterInstance(new GamePoints(_finishPoint, _checkPoints, _coins, _botsRoad));

            builder.Register<GameFinishController>(Lifetime.Singleton); // под вопросом...
            builder.Register<GameRestartController>(Lifetime.Singleton); // под вопросом...
            //builder.Register<GameManager>(Lifetime.Singleton);
            builder.RegisterEntryPoint<BillboardManager>().AsSelf();

            builder.RegisterFactory<UnitInfoUI>(container => () =>
            {
                return container.Instantiate(_unitInfoUIPrefab);
            }, Lifetime.Transient);

            builder.RegisterFactory<Character>(container => () =>
            {
                return container.Instantiate(_characterPrefab);
            }, Lifetime.Transient);

            builder.RegisterFactory<NavMeshCharacter>(container => () =>
            {
                return container.Instantiate(_navMeshCharacterPrefab);
            }, Lifetime.Transient);
        }
    }
}
