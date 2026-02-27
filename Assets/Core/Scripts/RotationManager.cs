using UnityEngine;

namespace Core.Scripts
{
    public class RotationManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Object with IsGrabbed script")] private IsGrabbed isGrabbed;

        [Header("Position of hands")] 
        [SerializeField] private Transform leftHand; 
        [SerializeField] private Transform rightHand;
        private Quaternion _lookRotation;
        private Vector3 _direction; 
        void Update()
        {
            if (isGrabbed.BothGrabbed)
            {
                Vector3 midPoint = TrackingPosition.CalculateMiddlePoints(leftHand, rightHand);
                _direction = new Vector3(transform.position.x - midPoint.x, 
                    0f, 
                    transform.position.z - midPoint.z).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = _lookRotation;
            }
        }
    }
}
