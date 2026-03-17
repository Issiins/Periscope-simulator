using System;
using System.Text;
using UnityEngine;

namespace Core.Scripts.Periscope_Tranformation
{
    public abstract class BaseParameter : MonoBehaviour
    {
        protected StringBuilder stringBuilder;
        public abstract string  GetValue();

        protected virtual void Start()
        {
            stringBuilder = new StringBuilder();
        }
    }
}