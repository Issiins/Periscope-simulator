using System.Text;
using Core.Scripts.Data;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Scripts
{
    public class FIndObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Camera periscopeCamera;
        [SerializeField] private EnvironmentObject[] environmentObjects;
        [SerializeField] private float threshHold = 0.98f;
        [SerializeField] private float requiredTime = 3.0f;
        [SerializeField] private AudioClip successBell;
        [Range(0f, 1f)] [SerializeField] private float bellVolume = 0.5f;
        
        [Header("UI")]
        [SerializeField] private Image objectImage;
        [SerializeField] private TMP_Text objectText;
        
        [FormerlySerializedAs("_idx")]
        [Header("Live Stats")]
        [SerializeField, ReadOnly] private int idx = 0;
        [SerializeField, ReadOnly] private float lookTimer = 0f;
        [SerializeField, ReadOnly] private float currentDot;
        private void Start()
        {
            if (environmentObjects.Length > 0) 
            {
                UpdateUI(environmentObjects[idx]);
            }
        }
        private void Update()
        {
            if (environmentObjects.Length == 0 || idx >= environmentObjects.Length) return;
            var targetPosition = environmentObjects[idx].pos.position;
            Vector3 directionToTarget = (targetPosition - periscopeCamera.transform.position).normalized;
            currentDot = Vector3.Dot(directionToTarget, periscopeCamera.transform.forward);
            if (currentDot > threshHold)
            {
                lookTimer += Time.deltaTime;

                if (lookTimer < requiredTime) return;
                idx++;         
                lookTimer = 0f;
                if (successBell != null)
                {
                    AudioSource.PlayClipAtPoint(successBell, transform.position, bellVolume);
                }
                if (idx < environmentObjects.Length)
                    UpdateUI(environmentObjects[idx]);
                else
                    objectText.text = "All Targets Found!";
            }
            else
            {
                if (lookTimer >= 0) return;
                lookTimer = 0f;
            }
        }
        private void UpdateUI(EnvironmentObject target)
        {
            objectImage.sprite = target.objectImage;
            StringBuilder sb =  new StringBuilder();
            sb.Append($"Find: {target.name}!");
            objectText.text = sb.ToString();
            sb.Clear();
        }
    }
}