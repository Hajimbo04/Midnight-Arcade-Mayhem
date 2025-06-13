using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallResetter : MonoBehaviour
{
    public Transform respawnPoint;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // wait before resetting

        if (transform.position.y < -1f) // only reset if it fell
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            transform.position = respawnPoint.position;
            transform.rotation = Quaternion.identity;

            yield return null;

            rb.isKinematic = false;
        }
    }
}

