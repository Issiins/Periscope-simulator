using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace Core.Scripts.Control_Table
{
    [RequireComponent(typeof(HingeJoint))]
    public class RotationChecker : MonoBehaviour
    {
        [Tooltip("In seconds")][SerializeField] private float loopUpdateRate = 0.5f;
        [SerializeField] private bool rotationOnOwnAxis; // boolean to check if object will rotate around own axis 
        private XRDirectInteractor _hand;
        private HingeJoint _joint;
        private float _rotationQuotient;
        private float _lastHandX;
        private float _accumulatedDelta;
        private Quaternion _startObjectRotation;
        private bool _initialized;

        public float RotationQuotient => _rotationQuotient;
        private void Awake()
        {
            _joint = GetComponent<HingeJoint>();
StartCoroutine(UpdateLoop());
        }

        private IEnumerator UpdateLoop()
        {
            while (true)
            {
                EvaluateRotationQuotient();
                yield return new WaitForSeconds(loopUpdateRate);
            }
        }
        private void Update()
        {
            if (rotationOnOwnAxis && _hand is not null && _hand.hasSelection) // checks if switch is grabbed
            {
                if (!_initialized) // initialize on first frame after starting grabbing
                {
                    _lastHandX = _hand.transform.localEulerAngles.x; // saves hand rotation
                    _accumulatedDelta = 0f;                        
                    _startObjectRotation = transform.localRotation;
                    _initialized = true;
                }
                FollowHandRotation(_joint.axis);
            }
            else
            {
                _initialized = false;
            }
        }

        private void EvaluateRotationQuotient()
        {
            var currentQuotient = _joint.angle;
            if (IsValidAngle(currentQuotient))
            {
                currentQuotient = 0f; 
            }
            Debug.Log($"in object '{gameObject.name}' currentQuotient value is: "+currentQuotient);
            var range = Mathf.Abs(_joint.limits.min - _joint.limits.max);
            currentQuotient = 2f * ((currentQuotient - _joint.limits.min) / range);
            currentQuotient -= 1f;
            _rotationQuotient = Mathf.Clamp(currentQuotient, -1f, 1f);
        }

        private void FollowHandRotation(Vector3 switchAxis)
        {
            if (_hand is null) return;
            if (switchAxis == Vector3.zero) return; // guard against NaN

            float handX = _hand.transform.localEulerAngles.x;
            float deltaX = Mathf.DeltaAngle(_lastHandX, handX); // finds delta between staring point and current point rotations 
            _lastHandX = handX;

            _accumulatedDelta += deltaX;
            _accumulatedDelta = Mathf.Clamp(_accumulatedDelta, _joint.limits.min, _joint.limits.max);

            var targetRotation = _startObjectRotation * Quaternion.AngleAxis(_accumulatedDelta, switchAxis);
            if (IsValidQuaternion(targetRotation)) // guard before assigning
                transform.localRotation = targetRotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactor = other.GetComponent<XRDirectInteractor>();
            if (interactor is null) return;
            _hand = interactor;
            _initialized = false;
            
        }

        private void OnTriggerExit(Collider other)
        {
            var interactor = other.GetComponent<XRDirectInteractor>();
            if (interactor is not null && interactor == _hand)
            {
                _hand = null;
                _initialized = false;
            }
        }
        private bool IsValidQuaternion(Quaternion q)
        {
            return !IsValidAngle(q.x) && !IsValidAngle(q.y) && !IsValidAngle(q.z) && !IsValidAngle(q.w);
        }

        private bool IsValidAngle(float angle)
        {
            return float.IsNaN(angle) || float.IsInfinity(angle);
        }
    }
}