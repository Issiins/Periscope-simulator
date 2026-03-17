using System;
using System.Collections;
using System.Text;
using Core.Scripts.Control_Table;
using UnityEngine;

namespace Core.Scripts.Periscope_Tranformation
{
    public class VerticalMovement : BaseParameter
    {
        [SerializeField] private RotationChecker rotationChecker;
        [SerializeField] private float maxHeightDelta = 15f;
        [SerializeField, Tooltip("Maximum speed of periscope movement – m/s")] private float maxMoveSpeed = 0.6f;
        [SerializeField] private float midOffset = 0.1f;
        private Camera _cam;
        private float _currentSpeed;
        private float _maxHeight;
        private float _minHeight;
        private float _targetHeight;
        private float _speedAmplifier;
        private float _currentHeight;

        protected override void Start()
        {
            base.Start();
            _cam = transform.parent.parent.GetComponent<Camera>();
            _minHeight = _cam.transform.localPosition.y;
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
            _cam.transform.localPosition = new Vector3(_cam.transform.localPosition.x, _currentHeight, _cam.transform.localPosition.z); 
        }

        private IEnumerator SpeedUpdate()
        {
            while (true)
            {
                _speedAmplifier = rotationChecker.RotationQuotient;
                yield return new WaitForSeconds(0.5f);
            }
        }

        public override string GetValue()
        {
            float value = (float) Math.Round(_cam.transform.localPosition.y - _minHeight,2);
            stringBuilder.Append(value);
            stringBuilder.Append(" m");
            string returnValue = stringBuilder.ToString();
            stringBuilder.Clear();
            return returnValue;
        }
    }
}