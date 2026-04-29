using System;
using UnityEngine;

namespace Core.Scripts
{
    /// <summary>
    /// Handles the rotation of the periscope based on the midpoint between two hands.
    /// </summary>
    public class RotationManager : MonoBehaviour
    {
        [SerializeField] private IsGrabbed isGrabbed;
        [SerializeField] private HandTransform handPosition;
        [Header("Tuning")]
        [Range(1f, 20f)]
        [SerializeField] private float smoothness = 8f;
        private Vector3 _currentDirection;
        void Update()
        {
            if (!isGrabbed.BothGrabbed) return;
            Vector3 midPoint = TrackingPosition.CalculateMiddlePoints(handPosition.LeftHand, handPosition.RightHand);
            Vector3 targetDir = new Vector3(
                transform.position.x - midPoint.x, 
                0f, 
                transform.position.z - midPoint.z
            ).normalized;
            _currentDirection = Vector3.Lerp(_currentDirection, targetDir, Time.deltaTime * smoothness);
            if (_currentDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_currentDirection);
            }
        }
    }
}
