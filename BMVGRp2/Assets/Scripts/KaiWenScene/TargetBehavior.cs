using UnityEngine;
using System;

public class TargetBehavior : MonoBehaviour
{
    public bool isBomb = false;
    public float explosionRadius = 3f;
    public int pointValue = 10;
    public float moveSpeed = 1f;
    public BoxCollider movementBounds;

    private Vector3 targetPosition;
    public event Action OnDestroyed;

    public void Initialize(BoxCollider bounds)
    {
        movementBounds = bounds;
    }

    private void Start()
    {
        PickNewTargetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            PickNewTargetPosition();
        }
    }

    void PickNewTargetPosition()
    {
        if (movementBounds == null)
        {
            Debug.LogWarning("Movement bounds not set!");
            return;
        }

        Bounds b = movementBounds.bounds;
        float newX = UnityEngine.Random.Range(b.min.x, b.max.x);
        float newY = UnityEngine.Random.Range(b.min.y, b.max.y);
        float newZ = transform.position.z;

        targetPosition = new Vector3(newX, newY, newZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            if (isBomb)
            {
                Explode();
            }
            else
            {
                ScoreManager.Instance.AddScore(pointValue);
            }

            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        int netScore = 0;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.gameObject == this.gameObject || hit.CompareTag("Dart"))
                continue;

            TargetBehavior tb = hit.GetComponent<TargetBehavior>();
            if (tb == null || !hit.gameObject.scene.IsValid())
                continue;

            netScore += tb.isBomb ? -tb.pointValue : tb.pointValue;
            Destroy(hit.gameObject);
        }

        ScoreManager.Instance.AddScore(netScore);
    }
}
