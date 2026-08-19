using Assets._Scripts.UI._1MenuWindow;
using Assets.ScriptableObjects.Language;

namespace Assets._Scripts.GameControllers.Menu
{
    public class MenuController
    {
        private MenuModel _menuModel;
        //private MenuTabsView _menuTabsView;

        public MenuController(MenuModel menuModel)
        {
            _menuModel = menuModel;
        }

        public void SetLeanguage(LanguageConfig languageConfig)
        {

        }
    }
}
