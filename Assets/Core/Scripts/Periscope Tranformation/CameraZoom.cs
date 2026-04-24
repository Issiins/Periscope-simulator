using System;
using System.Text;
using Core.Scripts.Control_Table.Control;
using UnityEngine;

namespace Core.Scripts.Periscope_Tranformation
{
    public class CameraZoom : BaseParameter
    {
        [SerializeField] private RotationChecker rotationChecker;
        [SerializeField] private float maxZoomFactor = 3f;
        private float _currentZoomFactor;
        private Camera _cam;
        private float InitFov { get; set; }  // initial FOV at the start.
        protected override void Start()
        {
            base.Start();
            _cam = transform.parent.parent.GetComponent<Camera>();
            InitFov = _cam.fieldOfView;
        }
        private void Update()
        {
            float input = rotationChecker.RotationQuotient;
            float tValue = (input + 1f) * 0.5f;
            _currentZoomFactor = Mathf.Lerp(1f, maxZoomFactor, tValue);
            _cam.fieldOfView = Zoom(_currentZoomFactor);
        }
        /// <summary>
        /// Zooms by factor
        /// Converts zoom factor value to FOV value
        /// </summary>
        /// <param name="zoomFactor">Factor how much camera is zoomed</param>
        /// <returns>Returns fov value to set</returns>
        private float Zoom(float zoomFactor)
        {
            float baseTan = Mathf.Tan(Mathf.Deg2Rad * InitFov * 0.5f);
            return 2f * Mathf.Atan(baseTan / zoomFactor) * Mathf.Rad2Deg;
        }

        public override string GetValue()
        {
            var value = (float) Math.Round(_currentZoomFactor,2);
            StringBuilder.Append(value);
            StringBuilder.Append('x');
            string returnValue = StringBuilder.ToString();
            StringBuilder.Clear();
            return returnValue;
        }
    }
}