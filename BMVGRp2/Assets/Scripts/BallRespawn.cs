using UnityEngine;


public class BallRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float delay = 2f;

    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Floor") && !grabInteractable.isSelected)
        {
            Debug.Log("Will respawn in " + delay + " seconds");
            Invoke(nameof(Respawn), delay);
        }
    }

    void Respawn()
    {
        Debug.Log("Respawning!");

        // Temporarily disable grab interaction to avoid conflicts
        grabInteractable.enabled = false;

        // Disable physics so we can teleport safely
        rb.isKinematic = true;

        // Move the ball
        transform.position = respawnPoint.position;

        // Reset motion
        rb.linearVelocity = Vector3.zero;

        // Check if we can safely reset angular velocity
        if (!rb.isKinematic)
            rb.angularVelocity = Vector3.zero;

        // Re-enable physics and grabbing
        rb.isKinematic = false;
        grabInteractable.enabled = true;
    }
}
