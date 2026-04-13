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
    [SerializeField] private float shiftStrength = 0.5f;
    private Vector3 _leftQuadBasePos;
    private Vector3 _rightQuadBasePos;

    void Start()
    {
        _leftQuadBasePos  = leftQuad.localPosition;
        _rightQuadBasePos = rightQuad.localPosition;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, playerRig.position) > range) return;
        float headRoll = playerRig.localEulerAngles.z;
        if (headRoll > 180f) headRoll -= 360f;
        float shift = headRoll * shiftStrength * Time.deltaTime; 
        leftQuad.localPosition  = _leftQuadBasePos  + new Vector3(0f,  shift, 0f);
        rightQuad.localPosition = _rightQuadBasePos + new Vector3(0f, -shift, 0f);
    }
}
