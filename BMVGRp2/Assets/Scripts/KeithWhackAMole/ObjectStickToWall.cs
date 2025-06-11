using UnityEngine;

public class ObjectStickToWall : MonoBehaviour
{
    private Vector3 initialCollisionPosition;
    private bool isStuck = false;
    private bool isGrabbed = false;
    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        if (myRigidbody == null)
        {
            Debug.LogError("Rigidbody component is missing from the object.");
        }
    }

    private void Update()
    {
        if (isStuck && isGrabbed)
        {
            // Keep the object at the initial collision position
            transform.position = initialCollisionPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGrabbed && !isStuck && collision.gameObject.CompareTag("Wall"))
        {
            // Save the position of the initial collision
            initialCollisionPosition = transform.position;
            isStuck = true;

            // Optionally, stop the object's movement
            if (myRigidbody != null)
            {
                myRigidbody.linearVelocity = Vector3.zero;
                myRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isStuck && collision.gameObject.CompareTag("Wall"))
        {
            // Allow the object to be moved again
            isStuck = false;
        }
    }

    // Call this method when the object is grabbed
    public void OnGrab()
    {
        isGrabbed = true;
    }

    // Call this method when the object is released
    public void OnRelease()
    {
        isGrabbed = false;
        isStuck = false;
    }
}
