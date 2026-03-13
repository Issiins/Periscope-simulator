using System;
using System.Collections;
using UnityEngine;

namespace Core.Scripts.Control_Table
{
    [RequireComponent(typeof(HingeJoint))]
    public class RotationChecker : MonoBehaviour
    {
        [Tooltip("In seconds")][SerializeField] private float loopUpdateRate = 0.5f;
        private HingeJoint _joint;
        private float _rotationQuotient;
        public float RotationQuotient => _rotationQuotient;
        private void Start()
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
        /// <summary>
        /// Calculates the quotient of the switch angle
        /// Return range [-1,1]
        /// </summary>
        private void EvaluateRotationQuotient()
        {
            var currentQuentient = _joint.angle;
            var range = Mathf.Abs(_joint.limits.min - _joint.limits.max);
            currentQuentient = 2f*((currentQuentient - _joint.limits.min)/range);
            currentQuentient -= 1f;
            _rotationQuotient = Mathf.Clamp(currentQuentient, -1f, 1f);
        }
    }
}