using System;
using UnityEngine;

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
    private Vector3 _leftQuadBasePos;
    private Vector3 _rightQuadBasePos;
    private float _currentShift;

    void Start()
    {
        _leftQuadBasePos  = leftQuad.localPosition;
        _rightQuadBasePos = rightQuad.localPosition;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerRig.position) > range)
        {
            _currentShift = Mathf.Lerp(_currentShift, 0f, Time.deltaTime * smoothSpeed);
            ApplyShift(_currentShift);
            return;
        }
        float headRoll = playerRig.localEulerAngles.z;
        if (headRoll > 180f) headRoll -= 360f;
        headRoll = Mathf.Clamp(headRoll, -maxRollDegrees, maxRollDegrees);
        if (Mathf.Abs(headRoll) < deadZone) headRoll = 0f;
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