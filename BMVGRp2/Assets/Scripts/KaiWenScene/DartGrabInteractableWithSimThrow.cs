using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DartGrabInteractableWithSimThrow : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    [Header("Simulated Throw Settings")]
    public Vector3 simulatedVelocity = new Vector3(0.1f, 1.2f, 5.5f);
    public Vector3 simulatedAngularVelocity = new Vector3(0, 10f, 30f);

    private bool isSimulator = false;

    protected override void Awake()
    {
        base.Awake();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        DetectIfSimulator();
    }

    private void DetectIfSimulator()
    {
        string xrDevice = XRSettings.loadedDeviceName?.ToLower();

        if (string.IsNullOrEmpty(xrDevice) || xrDevice.Contains("mock") || xrDevice.Contains("simulator"))
        {
            isSimulator = true;
        }
        else
        {
            isSimulator = false;
        }

        Debug.Log("XR Device: " + XRSettings.loadedDeviceName + ", isSimulator = " + isSimulator);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (isSimulator)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                StartCoroutine(ApplySimulatedThrowNextFrame(rb));
            }
        }

        // Start teleport-back coroutine regardless of simulator
        StartCoroutine(TeleportBackAfterDelay(3f)); // You can adjust the delay
    }

    private System.Collections.IEnumerator ApplySimulatedThrowNextFrame(Rigidbody rb)
    {
        yield return null; // wait one frame so isKinematic is reset

        rb.linearVelocity = transform.forward * simulatedVelocity.z +
                      transform.up * simulatedVelocity.y +
                      transform.right * simulatedVelocity.x;

        rb.angularVelocity = simulatedAngularVelocity;

        Debug.Log("Simulated throw applied: " + rb.linearVelocity);
    }

    private System.Collections.IEnumerator TeleportBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset position and rotation
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

        // Reset rigidbody velocity
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // temporarily disable physics
        }

        // Disable & re-enable to reset grab state
        interactionManager.UnregisterInteractable((UnityEngine.XR.Interaction.Toolkit.Interactables.IXRInteractable)this);
        yield return null;
        interactionManager.RegisterInteractable((UnityEngine.XR.Interaction.Toolkit.Interactables.IXRInteractable)this);

        if (rb != null)
        {
            rb.isKinematic = false; // re-enable physics
        }

        Debug.Log("Dart reset and re-registered.");
    }
}