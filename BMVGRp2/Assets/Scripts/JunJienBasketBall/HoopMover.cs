using UnityEngine;

public class HoopMover : MonoBehaviour
{
    public float moveRangeX = 1f;
    public float moveRangeY = 0.5f;
    public float moveSpeed = 1.5f;
    public Scoremanager scoreManager; // You will drag your ScoreManager object here

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
    }

    void Update()
    {
        int score = scoreManager.currentScore;

        if (score < 16)
        {
            // Don't move
            transform.position = startPosition;
        }
        else if (score >= 16 && score < 30)
        {
            // Move left and right
            float offset = Mathf.PingPong(Time.time * moveSpeed, moveRangeX * 2) - moveRangeX;
            transform.position = startPosition + new Vector3(offset, 0f, 0f);
        }
        else
        {
            // Move randomly on X and Y
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                SetNewTargetPosition();
            }
        }
    }

    void SetNewTargetPosition()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);
        targetPosition = startPosition + new Vector3(randomX, randomY, 0f);
    }
}
