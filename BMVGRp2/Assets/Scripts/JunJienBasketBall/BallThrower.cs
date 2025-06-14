using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallThrower : MonoBehaviour
{
    public float throwForce = 10f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool isHeld = false;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Subscribe to grab/release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        if (isHeld && Input.GetKeyDown(KeyCode.P))
        {
            ThrowBall();
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    void ThrowBall()
    {
        // Release the ball before applying force
        grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);
        
        // Apply forward force based on controller/hand direction
        Vector3 throwDirection = grabInteractable.firstInteractorSelecting.transform.forward;
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
