using System;
using System.Collections;
using Core.Scripts.Control_Table;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Scripts.Periscope_Tranformation
{
    public class CameraDoF : BaseParameter
    {
         [SerializeField] private PositionChecker positionChecker;
         [SerializeField] private float maxFocusLenght = 300f;
         [SerializeField] private Volume volume;
         private float _coefficient;
         private DepthOfField _depthOfField;
        
         protected override void Start()
         {
             base.Start();
             volume.profile.TryGet(out _depthOfField);
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

         public override string GetValue()
         {
             stringBuilder.Append(Math.Round(_depthOfField.focalLength.value, 2));
             var returnString = stringBuilder.ToString();
             stringBuilder.Clear();
             return returnString;
         }
    }
}