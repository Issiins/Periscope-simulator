using System;
using System.Collections;
using Core.Scripts.Control_Table.Control;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Scripts.Periscope_Tranformation
{
    /// <summary>
    /// Class made for depth of field (DoF) parameter.
    /// Changes value of DoF between range [0; maxFocusLenght].
    /// </summary>
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
             _depthOfField.focalLength.value = CalculateValueDoF();
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
             StringBuilder.Append(Math.Round(_depthOfField.focalLength.value, 2));
             var returnString = StringBuilder.ToString();
             StringBuilder.Clear();
             return returnString;
         }
         private float CalculateValueDoF() => maxFocusLenght*0.5f*(_coefficient + 1); 
    }
}