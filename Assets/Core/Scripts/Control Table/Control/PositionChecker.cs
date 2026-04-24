using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Control_Table.Control
{
    /// <summary>
    /// Checks position of the joint and normalize the values
    /// </summary>
    public class PositionChecker : MonoBehaviour
    {
        [Tooltip("In seconds")][SerializeField] private float loopUpdateRate = 0.5f;
        private ConfigurableJoint _joint;
        private float _distanceCoeff;
        private float _range;
        public float DistanceCoeff => _distanceCoeff;
        private void Start()
        {
            _distanceCoeff = 0f;
            _joint = GetComponent<ConfigurableJoint>();
            _range = _joint.linearLimit.limit;
            StartCoroutine(UpdateLoop());
        }

        private IEnumerator UpdateLoop()
        {
            while (true)
            {
                EvaluatePositionCoeff();
                yield return  new WaitForSeconds(loopUpdateRate);
            }
        }
        private void EvaluatePositionCoeff()
        {
            var currentDistance = GetJointDistance();
            float normalizedDistance = currentDistance / _range;
            _distanceCoeff = Mathf.Clamp(normalizedDistance, -1f, 1f); // normalize value
        }
        private float GetJointDistance()
        {
            Vector3 anchorA = _joint.transform.TransformPoint(_joint.anchor);
            Vector3 anchorB = _joint.connectedAnchor;
            Vector3 delta = anchorA-anchorB;
            Vector3 axisWorld = _joint.transform.TransformDirection(_joint.axis);
            float signedDistance = Vector3.Dot(delta, axisWorld);
            return signedDistance;
        }
    }
}