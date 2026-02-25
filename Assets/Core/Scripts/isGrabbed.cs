using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class isGrabbed : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable leftHandle; 
    [SerializeField] private XRGrabInteractable rightHandle;
    private void Update()
    {
        bool bothGrabbed = leftHandle.isSelected && rightHandle.isSelected;
        
        if (bothGrabbed)
        {
            Debug.Log("Both handles grabbed - Periscope operational");
        }
        else
        {
            if (leftHandle.isSelected || rightHandle.isSelected)
            {
                Debug.Log("Waiting for second hand...");
            }
        }
    }
}
