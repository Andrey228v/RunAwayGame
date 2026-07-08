using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameControllers.GameShop
{
    public interface IUnlockSkin
    {
        public string Name { get; set; }
        public bool IsUnlock {  get; }
        public void UnlockSkin();
    }
}
