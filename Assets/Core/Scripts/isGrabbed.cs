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
        private bool _bothGrabbed;
        public bool BothGrabbed => _bothGrabbed;
        private void Update()
        {
            bool leftGrabbed = leftHandle.isSelected;
            bool rightGrabbed = rightHandle.isSelected;
            _bothGrabbed = leftGrabbed && rightGrabbed;
            if (Vector3.Distance(handPos.LeftHand.position, leftHandle.transform.position) > maxDistance)
            {
                leftHandle.interactionManager.CancelInteractableSelection((IXRSelectInteractable)leftHandle);
            }
            else if (Vector3.Distance(handPos.RightHand.position, leftHandle.transform.position) > maxDistance)
            {
                rightHandle.interactionManager.CancelInteractableSelection((IXRSelectInteractable)rightHandle);
            }
        }
    }
}
