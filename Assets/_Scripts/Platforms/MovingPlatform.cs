using DG.Tweening;
using UnityEngine;

namespace Assets._Scripts.Platforms
{
    class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private float _moveDistance = 3f;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private Axis _moveAxis = Axis.X;



        private Tween _tweener;
        private Vector3 _startPosition;
        private Vector3 _endPosition;

        private enum Axis { X, Y, Z }

        private void Start()
        {
            _startPosition = transform.position;
            _endPosition = CalculateEndPosition();
        }

        private Vector3 CalculateEndPosition()
        {
            Vector3 endPos = _startPosition;

            switch (_moveAxis)
            {
                case Axis.X: endPos.x += _moveDistance; break;
                case Axis.Y: endPos.y += _moveDistance; break;
                case Axis.Z: endPos.z += _moveDistance; break;
            }

            return endPos;
        }


    }
}
