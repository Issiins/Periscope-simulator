using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class IsGrabbed : MonoBehaviour
    {
        [SerializeField] private XRGrabInteractable leftHandle; 
        [SerializeField] private XRGrabInteractable rightHandle;
        private bool _bothGrabbed;
        public bool BothGrabbed => _bothGrabbed;
        private void Update()
        {
            _bothGrabbed = leftHandle.isSelected && rightHandle.isSelected;
        }
    }
}
