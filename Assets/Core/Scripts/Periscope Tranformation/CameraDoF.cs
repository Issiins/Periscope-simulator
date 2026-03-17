using System;
using System.Collections;
using Core.Scripts.Control_Table;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Scripts.Periscope_Tranformation
{
    public class CameraDoF : MonoBehaviour
    {
        [SerializeField] private PositionChecker positionChecker;
        [SerializeField] private float maxFocusLenght = 300f;
         private float _coefficient;
         private Volume _volume;
         private DepthOfField _depthOfField;
        
         private void Start()
         {
             _volume = GetComponentInChildren<Volume>();
             _volume.profile.TryGet(out _depthOfField);
             _coefficient = positionChecker.DistanceCoeff;
             StartCoroutine(CoeffientUpdate());
         }

         void Update()
         {
             _depthOfField.focalLength.value = maxFocusLenght*0.5f*(_coefficient + 1); 
         }
         private IEnumerator CoeffientUpdate()
         {
             while (true)
             {
                 _coefficient = positionChecker.DistanceCoeff;
                 yield return new WaitForSeconds(0.5f);
             }
         }
       
    }
}