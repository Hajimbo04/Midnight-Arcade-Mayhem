using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockRotationOnGrab : MonoBehaviour
{
    private Quaternion initialRotation;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        initialRotation = transform.rotation;

        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Lock rotation during grab
        transform.rotation = initialRotation;
        grabInteractable.trackRotation = false; // Prevent automatic rotation to match hand
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Restore to original rotation on release
        transform.rotation = initialRotation;
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
