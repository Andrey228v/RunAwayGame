using System;

namespace Assets._Scripts.ObjectsScripts.UI.GamePanel
{
    public enum WindowType
    {
        InterfacePanel = 0,
        MenuPanel = 1,
        WinPanel = 2,
    }

    public class GamePanelModel
    {
        private WindowType _currentWindowType;
        private int _indexWindow = 0;

        public event Action<WindowType> OnWindowChange;

        public void SetWindow(WindowType windowType) 
        {
            _currentWindowType = windowType;
            OnWindowChange?.Invoke(windowType);
        }
    }
}
