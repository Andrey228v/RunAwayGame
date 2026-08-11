using Assets._Scripts.EnteryPoints;
using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.GameMVP;
using Assets._Scripts.GameMVP.Achievments;
using Assets._Scripts.GameMVP.Language;
using Assets._Scripts.GameMVP.Levels;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using Assets._Scripts.UI;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad;
using ECM2;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class RootScope : LifetimeScope
    {
        [SerializeField] private List<SceneGroupHandle> _sceneGroupHandle;
        [SerializeField] private LoadScreenView _loadScreenView;

        [SerializeField] private UnitInfoUIView _unitInfoUIPrefab;
        [SerializeField] private Character _characterPrefab; // префаб Unit, тут надо префаб делать пустой без Player и Bot. Потом переделать.
        [SerializeField] private NavMeshCharacter _navMeshCharacterPrefab;

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
            var modelLanguage = CreateModelLanguage();

            builder.RegisterInstance(modelLanguage);

            builder.RegisterInstance(_sceneGroupHandle);
            builder.RegisterInstance(_loadScreenView);
            builder.RegisterEntryPoint<RootEntryPoint>().AsSelf();
            builder.Register<EasySaveSystem>(Lifetime.Singleton);
            builder.Register<LoadManager>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameSaveLoadService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<LevelsController>().AsSelf();
            builder.RegisterEntryPoint<AchievmentsController>().AsSelf();
            builder.RegisterEntryPoint<ShopController>().AsSelf();
            builder.Register<WalletController>(Lifetime.Singleton).AsSelf();
            builder.Register<IGameLogger>(container => new UnityLogger("Game"), Lifetime.Singleton);
            builder.RegisterEntryPoint<GameLoopService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<LevelLoopService>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<LevelsDictinaryModel>().AsSelf();
            builder.RegisterEntryPoint<AchievmentDictinaryModel>().AsSelf();
            builder.RegisterEntryPoint<BillboardManager>().AsSelf();

            builder.RegisterEntryPoint<LanguageController>().AsSelf();

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

        private LanguageModel CreateModelLanguage()
        {
            LanguageModel model = new LanguageModel();

            return model;
        }
    }
}
