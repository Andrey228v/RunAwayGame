using Assets._Scripts.GameControllers.GameShop;
using UnityEngine;

namespace Assets._Scripts.UI._1MenuWindow.ShopWindow
{
    public class ConditionSkinView : MonoBehaviour, IUnlockSkin
    {
        public string Name { get; set; }

        public bool IsUnlock { get; set; }

        public void UnlockSkin()
        {

        }
    }
}
