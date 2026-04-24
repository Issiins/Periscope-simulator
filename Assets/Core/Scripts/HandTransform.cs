using System;
using UnityEngine;
namespace Core.Scripts
{
    /// <summary>
    /// Serialize hands transforms
    /// </summary>
    [Serializable]
    public class HandTransform
    {
        [SerializeField] private Transform leftHand; 
        [SerializeField] private Transform rightHand;
        public Transform LeftHand => leftHand;
        public Transform RightHand => rightHand;
    }
}