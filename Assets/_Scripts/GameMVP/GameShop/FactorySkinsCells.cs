using Assets._Scripts.UI._1MenuWindow.ShopWindow;
using System;

namespace Assets._Scripts.GameControllers.GameShop
{
    public enum TypeView
    {
        ButtonBye,
        Condition
    }

    public enum ConditonType
    {
        FinishLevel,
        Achivment
    }

    public class FactorySkinsCells
    {
        private Func<UnitButtonSkinView> _unitButtonSkinViewFactory;
        private Func<ConditionSkinView> _conditionSkinViewFactory;

        public FactorySkinsCells(Func<UnitButtonSkinView> unitButtonSkinViewFactory,
            Func<ConditionSkinView> conditionSkinViewFactory) 
        {
            _unitButtonSkinViewFactory = unitButtonSkinViewFactory;
            _conditionSkinViewFactory = conditionSkinViewFactory;
        }

        public IUnlockSkin CreateCells(TypeView typeView, ConditonType conditonType)
        {
            IUnlockSkin skin = null;

            if (typeView == TypeView.ButtonBye) 
            {
                skin = new UnitButtonSkinView();
            }

            return null;
        }


    }
}
