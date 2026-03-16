using System;
using Core.Scripts.Control_Table;
using UnityEngine;

namespace Core.Scripts.Periscope_Tranformation
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private RotationChecker rotationChecker;
        [SerializeField] private float maxZoomFactor = 3f;
        private float _currentZoomFactor;
        private Camera _cam;
        
        private float BaseFov { get; set; }

        private void Start()
        {
            _cam = GetComponent<Camera>();
            BaseFov = _cam.fieldOfView;
        }

        private void Update()
        {
            float input = rotationChecker.RotationQuotient;

            float tValue = (input + 1f) * 0.5f;
            float zoomFactor = Mathf.Lerp(1f, maxZoomFactor, tValue);
            
            _cam.fieldOfView = Zoom(zoomFactor);
        }

        private float Zoom(float zoomFactor)
        {
            float baseTan = Mathf.Tan(Mathf.Deg2Rad * BaseFov * 0.5f);
            return 2f * Mathf.Atan(baseTan / zoomFactor) * Mathf.Rad2Deg;
        }
    }
}