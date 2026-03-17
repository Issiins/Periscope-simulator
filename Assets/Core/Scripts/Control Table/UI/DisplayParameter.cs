using System;
using System.Collections;
using Core.Scripts.Periscope_Tranformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Scripts.Control_Table.UI
{
    public class DisplayParameter : MonoBehaviour
    {
        [SerializeField] private BaseParameter displayParameter;
        private TextMeshProUGUI _textMeshProUGUI;
        private string _textToPrint;

        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
        private IEnumerator Start()
        {
            yield return null;
            _textToPrint = displayParameter.GetValue();
        }

        private void FixedUpdate()
        {
            _textToPrint = displayParameter.GetValue();
            _textMeshProUGUI.text = _textToPrint;
        }
    }
}