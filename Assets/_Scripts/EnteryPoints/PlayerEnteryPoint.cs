using Assets.Scripts.Camera;
using ECM2;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerEnteryPoint : IStartable, IDisposable
    {
        private Func<Character> _characterFactory;
        private CameraController _cameraController;

        public PlayerEnteryPoint(Func<Character> characterFactory, CameraController cameraController) 
        {
            _characterFactory = characterFactory;
            _cameraController = cameraController;
        }

        public void Start()
        {
            Character character = _characterFactory();

        }

        public void Dispose()
        {
            
        }
    }
}
