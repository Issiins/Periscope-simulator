using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class IsGrabbed : MonoBehaviour
    {
        [SerializeField] private XRGrabInteractable leftHandle;
        [SerializeField] private XRGrabInteractable rightHandle;
        [SerializeField] private HandTransform handPos;
        [SerializeField] private float maxDistance = 1.5f;
        private float _sqrMaxDistance;
        private bool _bothGrabbed;
        public bool BothGrabbed => _bothGrabbed;

        private void Start()
        {
            _sqrMaxDistance = maxDistance * maxDistance;
        }
        private void Update()
        {
           TryUngrabbing();
        }
        /// <summary>
        /// If user to far away, it will try to unselect the handles (aka unhold). 
        /// </summary>
        private void TryUngrabbing()
        {
            bool leftGrabbed = leftHandle.isSelected;
            bool rightGrabbed = rightHandle.isSelected;
            _bothGrabbed = leftGrabbed && rightGrabbed;
            float leftHandling = (leftHandle.transform.position - handPos.LeftHand.position).sqrMagnitude;
            float rightHandling = (rightHandle.transform.position - handPos.RightHand.position).sqrMagnitude;
            if (leftHandling > _sqrMaxDistance)
                leftHandle.interactionManager.CancelInteractableSelection((IXRSelectInteractable)leftHandle);
            else if (rightHandling > _sqrMaxDistance)
                rightHandle.interactionManager.CancelInteractableSelection((IXRSelectInteractable)rightHandle);
        }
    }
}
