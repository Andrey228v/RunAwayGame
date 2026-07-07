using Assets._Scripts.GameControllers;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets._Scripts.UI;
using Assets.Scripts.Points;
using ECM2;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private UnitInfoUIView _unitInfoUIPrefab;
        [SerializeField] private Character _characterPrefab; // префаб Unit, тут надо префаб делать пустой без Player и Bot. Потом переделать.
        [SerializeField] private NavMeshCharacter _navMeshCharacterPrefab;

        [SerializeField] private FinishPointView _finishPoint; // Переделать
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
            builder.RegisterInstance(new GamePoints(_finishPoint, _checkPoints, _coins, _botsRoad));

            builder.RegisterEntryPoint<BillboardManager>().AsSelf();

            builder.RegisterFactory<UnitInfoUIView>(container => () =>
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
