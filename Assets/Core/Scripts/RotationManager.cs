using System;
using UnityEngine;

namespace Core.Scripts
{
    public class RotationManager : MonoBehaviour
    {
        [SerializeField] private IsGrabbed isGrabbed;
        [SerializeField] private HandTransform handPosition;
        private Quaternion _lookRotation;
        private Vector3 _direction; 
        void Update()
        {
            if (!isGrabbed.BothGrabbed) return;
            Vector3 midPoint = TrackingPosition.CalculateMiddlePoints(handPosition.LeftHand, handPosition.RightHand);
            _direction = new Vector3(transform.position.x - midPoint.x, 
                0f, 
                transform.position.z - midPoint.z).normalized;
            Rotate();
        }
        private void Rotate()
        {
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = _lookRotation;
        }
    }
}
