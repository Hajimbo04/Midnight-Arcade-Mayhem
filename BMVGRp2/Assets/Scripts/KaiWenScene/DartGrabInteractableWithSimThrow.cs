using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DartGrabInteractableWithSimThrow : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Simulated Throw Settings")]
    public Vector3 simulatedVelocity = new Vector3(0.1f, 1.2f, 5.5f);
    public Vector3 simulatedAngularVelocity = new Vector3(0, 10f, 30f);

    private bool isSimulator = false;

    protected override void Awake()
    {
        base.Awake();
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
}
