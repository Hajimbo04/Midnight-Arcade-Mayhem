using UnityEngine;

public class HoopMover : MonoBehaviour
{
    public float moveRangeZ = 1f;   // ← was moveRangeX
    public float moveRangeY = 0.5f;
    public float moveSpeed  = 1.5f;

    public Scoremanager scoreManager;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition  = transform.position;
        targetPosition = startPosition;
    }

    void Update()
    {
        int score = scoreManager.currentScore;

        if (score < 20)
        {
            // stationary
            transform.position = startPosition;
        }
        else if (score < 40)
        {
            // move front ↔ back (world-Z)
            float offset = Mathf.PingPong(Time.time * moveSpeed, moveRangeZ * 2) - moveRangeZ;
            transform.position = startPosition + new Vector3(0f, 0f, offset);
        }
        else
        {
            // random Z-and-Y wandering
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        float randomZ = Random.Range(-moveRangeZ, moveRangeZ);
        float randomY = Random.Range(-moveRangeY, moveRangeY);

        targetPosition = startPosition + new Vector3(0f, randomY, randomZ);
    }
}
