using System;
using UnityEngine;
namespace Core.Scripts
{
    [Serializable]
    public class HandTransform
    {
        [SerializeField] private Transform leftHand; 
        [SerializeField] private Transform rightHand;
        public Transform LeftHand => leftHand;
        public Transform RightHand => rightHand;
    }
}