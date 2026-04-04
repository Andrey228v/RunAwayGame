using Assets._Scripts.UI;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers
{
    public class BillboardManager : ILateTickable
    {
        private bool _freezeX = false;  // Заморозить поворот по оси X
        private bool _freezeY = false;  // Заморозить поворот по оси Y
        private bool _freezeZ = true;

        private List<UnitInfoUI> _unitsUI = new List<UnitInfoUI>();
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
        private CameraController _cameraController;

        public void LateTick()
        {
            if (_unitsUI.Count > 0) 
            {
                if(_playerMoveDirectionCalculator  != null)
                {
                    foreach (UnitInfoUI ui in _unitsUI)
                    {
                        //ui.RotateToCamera(_playerMoveDirectionCalculator.GetMoveDirection());
                        ui.transform.LookAt(_cameraController.CameraCinemachine.transform);

                        //Vector3 direction = _cameraController.CameraCinemachine.transform.position - ui.transform.position;

                        //Quaternion targetRotation = Quaternion.LookRotation(direction);

                        //Vector3 euler = targetRotation.eulerAngles;

                        //if (_freezeX) euler.x = 0;
                        //if (_freezeY) euler.y = 0;
                        //if (_freezeZ) euler.z = 0;

                        //ui.transform.rotation = Quaternion.Euler(euler);
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
