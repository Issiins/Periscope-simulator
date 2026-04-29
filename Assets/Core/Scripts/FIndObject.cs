using System.Text;
using Core.Scripts.Data;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts
{
    /// <summary>
    /// Manages the identification of environment objects.
    /// Uses Dot Product to check if the periscope is aligned with a target.
    /// </summary>
    public class FIndObject : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The camera used to calculate the view direction.")]
        [SerializeField] private Camera periscopeCamera;
        [SerializeField] private EnvironmentObject[] environmentObjects;
        [Tooltip("How precisely the player must look at the object (1.0 is perfect).")]
        [SerializeField] private float threshHold = 0.98f;
        [SerializeField] private float requiredTime = 3.0f;
        [Header("Audio")]
        [SerializeField] private AudioClip successBell;
        [Range(0f, 1f)] [SerializeField] private float bellVolume = 0.5f;
        
        [Header("UI References")]
        [SerializeField] private Image objectImage;
        [SerializeField] private TMP_Text objectText;
        
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
                HandleTargetAcquired();
            }
            else
                lookTimer = 0f;
        }

        /// <summary>
        /// Logic triggered when a target is successfully 'locked on'.
        /// </summary>
        private void HandleTargetAcquired()
        {
            idx++;         
            lookTimer = 0f;
            if (successBell != null)
                AudioSource.PlayClipAtPoint(successBell, transform.position, bellVolume);
            if (idx < environmentObjects.Length)
                UpdateUI(environmentObjects[idx]);
            else
                objectText.text = "All Targets Found!";
        }

        /// <summary>
        /// Updates the HUD elements with the current target's information.
        /// </summary>
        private void UpdateUI(EnvironmentObject target)
        {
            objectImage.sprite = target.objectImage;
            StringBuilder sb =  new StringBuilder();
            sb.Append($"Find: {target.name}!");
            objectText.text = sb.ToString();
        }
    }
}