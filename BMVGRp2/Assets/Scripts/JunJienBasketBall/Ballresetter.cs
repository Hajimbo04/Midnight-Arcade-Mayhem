using UnityEngine;

public class Ballresetter : MonoBehaviour
{
    public Transform spawnPoint;              // Reference to the spawn location
    public float boundarySize = 10f;          // 10x10 area (5 units in each direction)
    public float resetDelay = 5f;             // Wait time before resetting
    private float outOfBoundsTimer = 0f;
    private bool isOutOfBounds = false;

    void Update()
    {
        Vector3 offset = transform.position - spawnPoint.position;

        // Check if ball is outside the 10x10 area (x and z only)
        if (Mathf.Abs(offset.x) > boundarySize / 2 || Mathf.Abs(offset.z) > boundarySize / 2)
        {
            if (!isOutOfBounds)
            {
                isOutOfBounds = true;
                outOfBoundsTimer = 0f;
            }

            outOfBoundsTimer += Time.deltaTime;

            if (outOfBoundsTimer >= resetDelay)
            {
                ResetBall();
            }
        }
        else
        {
            isOutOfBounds = false;
            outOfBoundsTimer = 0f;
        }
    }

    void ResetBall()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = spawnPoint.position;
        isOutOfBounds = false;
        outOfBoundsTimer = 0f;
    }
}
