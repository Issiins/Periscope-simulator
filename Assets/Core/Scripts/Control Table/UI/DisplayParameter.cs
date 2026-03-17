using System;
using Core.Scripts.Periscope_Tranformation;
using TMPro;
using UnityEngine;

namespace Core.Scripts.Control_Table.UI
{
    public class DisplayParameter : MonoBehaviour
    {
        [SerializeField] private BaseParameter displayParameter;
        private TextMeshProUGUI _textMeshProUGUI;
        private string _textToPrint;

        void Start()
        { 
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _textToPrint = displayParameter.GetValue();
        }

        private void FixedUpdate()
        {
            _textToPrint = displayParameter.GetValue();
            _textMeshProUGUI.text = _textToPrint;
        }
    }
}