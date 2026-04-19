using Assets._Scripts.EnteryPoints;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets._Scripts.UI._1MenuWindow.ShopWindow;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private MenuTabs _menuTabs;
        [SerializeField] private UnitButtonSkinView _unitButtonSkinViewPrefab;
        [SerializeField] private ConditionSkinView _conditionSkinViewPrefab;
        [SerializeField] private AchievementView _achievementPrefab;
        [SerializeField] private AchievmentsCellsView _achievmentsCellsViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEnteryPoint>();
            
            builder.Register<FactorySkinsCells>(Lifetime.Singleton);
            builder.Register<SkinController>(Lifetime.Singleton);

            builder.RegisterFactory<MenuTabs>(container => () =>
            {
                return container.Instantiate(_menuTabs);
            }, Lifetime.Singleton);

            builder.RegisterFactory<UnitButtonSkinView>(container => () =>
            {
                return container.Instantiate(_unitButtonSkinViewPrefab);
            }, Lifetime.Singleton);

            builder.RegisterFactory<ConditionSkinView>(container => () =>
            {
                return container.Instantiate(_conditionSkinViewPrefab);
            }, Lifetime.Singleton);

            builder.RegisterFactory<AchievementView>(container => () =>
            {
                return container.Instantiate(_achievementPrefab);
            }, Lifetime.Singleton);

            builder.RegisterFactory<AchievmentsCellsView>(container => () =>
            {
                return container.Instantiate(_achievmentsCellsViewPrefab);
            }, Lifetime.Singleton);

        }
    }
}
