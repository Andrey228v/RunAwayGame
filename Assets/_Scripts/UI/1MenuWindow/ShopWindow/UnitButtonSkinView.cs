using Assets._Scripts.GameControllers.GameShop;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.UI._1MenuWindow.ShopWindow
{
    public class UnitButtonSkinView : MonoBehaviour, IUnlockSkin
    {
        public string Name { get; set; }

        public bool IsUnlock { get; set; }

        public void UnlockSkin()
        {
            
        }
    }
}
