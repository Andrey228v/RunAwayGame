using Assets._Scripts.ObjectsScripts.Camera;
using Assets._Scripts.ObjectsScripts.Player;
using Assets._Scripts.UI;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers
{
    public class BillboardManager : ILateTickable, IDisposable
    {
        private List<UnitInfoUIView> _unitsUI = new List<UnitInfoUIView>();
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
                    foreach (UnitInfoUIView ui in _unitsUI)
                    {
                        if (ui != null)
                        {
                            ui.transform.LookAt(_cameraController.CameraCinemachine.transform);
                            ui.transform.Rotate(0, 180, 0);
                        }
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

        public void AddUnitUI(UnitInfoUIView ui)
        {
            _unitsUI.Add(ui);
        }

        public void RemoveUI(UnitInfoUIView ui) 
        {
            _unitsUI.Remove(ui);
        }
    }
}
