using System;
using System.Collections;
using Core.Scripts.Control_Table;
using UnityEngine;

namespace Core.Scripts.Periscope_Tranformation
{
    public class VerticalMovement : MonoBehaviour
    {
        [SerializeField] private RotationChecker rotationChecker;
        [SerializeField] private float maxHeightDelta = 15f;
        [SerializeField, Tooltip("Maximum speed of periscope movement – m/s")] private float maxMoveSpeed = 0.6f;
        [SerializeField] private float midOffset = 0.1f;
        private float _currentSpeed;
        private float _maxHeight;
        private float _minHeight;
        private float _targetHeight;
        private float _speedAmplifier;
        private float _currentHeight;

        private void Start()
        {
            _minHeight = transform.localPosition.y;
            _maxHeight = _minHeight + maxHeightDelta;
            _currentHeight = _minHeight;
            _currentSpeed = 0f;
            StartCoroutine(SpeedUpdate());
        }
        private void FixedUpdate()
        {
            MoveVertical();   
        }
        private void MoveVertical()
        {
            var concreteSpeed = Mathf.Abs(_speedAmplifier) > midOffset ? _speedAmplifier : 0;
            _currentSpeed = Math.Abs(maxMoveSpeed * concreteSpeed);
            _targetHeight = _speedAmplifier < 0 ? _minHeight : _maxHeight;
            _currentHeight = Mathf.MoveTowards(_currentHeight, _targetHeight, _currentSpeed * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x, _currentHeight, transform.localPosition.z); 
        }

        private IEnumerator SpeedUpdate()
        {
            while (true)
            {
                _speedAmplifier = rotationChecker.RotationQuotient;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}