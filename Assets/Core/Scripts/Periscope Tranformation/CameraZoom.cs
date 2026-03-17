using System;
using System.Text;
using Core.Scripts.Control_Table;
using UnityEngine;

namespace Core.Scripts.Periscope_Tranformation
{
    public class CameraZoom : BaseParameter
    {
        [SerializeField] private RotationChecker rotationChecker;
        [SerializeField] private float maxZoomFactor = 3f;
        private float _currentZoomFactor;
        private Camera _cam;
        
        private float BaseFov { get; set; }

        protected override void Start()
        {
            base.Start();
            _cam = transform.parent.parent.GetComponent<Camera>();
            BaseFov = _cam.fieldOfView;
        }

        private void Update()
        {
            float input = rotationChecker.RotationQuotient;

            float tValue = (input + 1f) * 0.5f;
            _currentZoomFactor = Mathf.Lerp(1f, maxZoomFactor, tValue);
            
            _cam.fieldOfView = Zoom(_currentZoomFactor);
        }

        private float Zoom(float zoomFactor)
        {
            float baseTan = Mathf.Tan(Mathf.Deg2Rad * BaseFov * 0.5f);
            return 2f * Mathf.Atan(baseTan / zoomFactor) * Mathf.Rad2Deg;
        }

        public override string GetValue()
        {
            var value = (float) Math.Round(_currentZoomFactor,2);
            stringBuilder.Append(value);
            stringBuilder.Append('x');
            string returnValue = stringBuilder.ToString();
            stringBuilder.Clear();
            return returnValue;
        }
    }
}