using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Scripts.Data
{
    [Serializable]
    public class EnvironmentObject
    {
        [SerializeField] public Transform pos;
        [SerializeField, CanBeNull] public string name;
        [SerializeField, CanBeNull] public Sprite objectImage;
    }
}