using UnityEngine;
using UnityEngine.InputSystem;

public class ClosetHiding : MonoBehaviour
{
    public Transform hidingSpot;       // Target position for the CAMERA (player's head)
    public Transform outsideSpot;      // Exit position for the CAMERA
    public GameObject xrRig;           // XR Origin (usually the root of the XR Rig)
    public Transform xrCamera;         // Main XR Camera (usually under the rig)

    private bool isHiding = false;
    private bool playerInsideTrigger = false;

    void Update()
    {
        if (playerInsideTrigger && Keyboard.current.nKey.wasPressedThisFrame)
        {
            ToggleHide();
        }
    }

    void ToggleHide()
    {
        Vector3 targetHeadPosition = isHiding ? outsideSpot.position : hidingSpot.position;

        // Calculate the current offset of the XR Camera relative to the XR Rig root
        Vector3 cameraOffset = xrCamera.position - xrRig.transform.position;

        // Subtract the camera offset from the target so the head lines up with hiding spot
        Vector3 newRigPosition = targetHeadPosition - cameraOffset;

        // Optional: Add a bit of inward offset so the head is safely inside the closet
        Vector3 inwardDirection = hidingSpot.forward * -0.2f; // move 20cm deeper
        newRigPosition += inwardDirection;

        // Apply the new position
        xrRig.transform.position = newRigPosition;

        isHiding = !isHiding;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInsideTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInsideTrigger = false;
    }
}
