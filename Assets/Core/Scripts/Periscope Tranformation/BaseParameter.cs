using System;
using System.Text;
using UnityEngine;

namespace Core.Scripts.Periscope_Tranformation
{
    /// <summary>
    /// Abstract class for periscope variables
    /// </summary>
    public abstract class BaseParameter : MonoBehaviour
    {
        protected StringBuilder StringBuilder;
        public abstract string  GetValue();
        protected virtual void Start() => StringBuilder = new StringBuilder();
    }
}