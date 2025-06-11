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
        rb.isKinematic = true;
        transform.position = respawnPoint.position;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;
    }
}
