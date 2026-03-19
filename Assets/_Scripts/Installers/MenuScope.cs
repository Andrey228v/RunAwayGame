using Assets._Scripts.UI._1MenuWindow;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private MenuTabs _menuTabs;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_menuTabs);
        }
    }
}
