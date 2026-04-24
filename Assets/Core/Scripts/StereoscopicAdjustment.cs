using System;
using UnityEngine;

namespace Core.Scripts
{
    /// <summary>
    /// Creates a fake 3D depth effect by shifting screen quads up and down.
    /// It tracks the player's head tilt and adjusts the view to mimic
    /// </summary>
    public class StereoscopicAdjustment : MonoBehaviour
    {
        [SerializeField] private Transform playerRig;
        [Header("Screens")]
        [SerializeField] private Transform leftQuad;
        [SerializeField] private Transform rightQuad;
        [Header("Settings")]
        [SerializeField] private float range = 0.5f;
        [SerializeField] private float shiftStrength = 1f;
        [SerializeField] private float maxRollDegrees = 15f;
        [SerializeField] private float deadZone = 1.5f;
        [SerializeField] private float smoothSpeed = 5f;
        private float _sqrRange;
        private Vector3 _leftQuadBasePos;
        private Vector3 _rightQuadBasePos;
        private float _currentShift;

        void Start()
        {
            _leftQuadBasePos  = leftQuad.localPosition;
            _rightQuadBasePos = rightQuad.localPosition;
            _sqrRange = range * range;
        }

        void Update()
        {
            if ((playerRig.position-transform.position).sqrMagnitude > _sqrRange)
            {
                _currentShift = Mathf.Lerp(_currentShift, 0f, Time.deltaTime * smoothSpeed);
                ApplyShift(_currentShift);
                return;
            }
            float headRoll = playerRig.localEulerAngles.z;
            if (headRoll > 180f) 
                headRoll -= 360f; // Normalize values
            headRoll = Mathf.Clamp(headRoll, -maxRollDegrees, maxRollDegrees);
            if (Mathf.Abs(headRoll) < deadZone) 
                headRoll = 0f;
            float targetShift = headRoll * shiftStrength * 0.001f;
            _currentShift = Mathf.Lerp(_currentShift, targetShift, Time.deltaTime * smoothSpeed);
            ApplyShift(_currentShift);
        }
        private void ApplyShift(float shift)
        {
            leftQuad.localPosition  = _leftQuadBasePos  + new Vector3(0f,  shift, 0f);
            rightQuad.localPosition = _rightQuadBasePos + new Vector3(0f, -shift, 0f);
        }
    }
}