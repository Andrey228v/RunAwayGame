using Assets._Scripts.UI;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers
{
    public class BillboardManager : ILateTickable, IDisposable
    {
        private List<UnitInfoUI> _unitsUI = new List<UnitInfoUI>();
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
        private CameraController _cameraController;

        public void Dispose()
        {
            _unitsUI.Clear();
        }

        public void LateTick()
        {
            if (_unitsUI.Count > 0) 
            {
                if(_playerMoveDirectionCalculator  != null)
                {
                    foreach (UnitInfoUI ui in _unitsUI)
                    {
                        ui.transform.LookAt(_cameraController.CameraCinemachine.transform);
                        ui.transform.Rotate(0, 180, 0);
                    }
                }
            }
        }

        public void SetDirectionCalculator(PlayerMoveDirectionCalculator directionCalculator)
        {
            _playerMoveDirectionCalculator = directionCalculator;
        }

        public void SetCameraController(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        public void AddUnitUI(UnitInfoUI ui)
        {
            _unitsUI.Add(ui);
        }

        public void RemoveUI(UnitInfoUI ui) 
        {
            _unitsUI.Remove(ui);
        }
    }
}
