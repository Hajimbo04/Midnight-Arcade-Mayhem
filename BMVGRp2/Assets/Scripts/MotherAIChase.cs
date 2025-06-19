using UnityEngine;
using UnityEngine.AI;

public class MotherAIChase : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 15f;
    public float fieldOfViewAngle = 120f;
    public float chaseSpeed = 5f;

    private NavMeshAgent agent;
    private motheraipatrol patrolScript;
    private bool isChasing = false;

    private AudioSource alertAudio;
    private bool hasPlayedAlert = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolScript = GetComponent<motheraipatrol>();
        alertAudio = GetComponent<AudioSource>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (distance <= detectionRadius && angle <= fieldOfViewAngle * 0.5f)
        {
            Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, detectionRadius))
            {
                Debug.Log("Ray hit: " + hit.transform.name);
                if (hit.transform == player)
                {
                    StartChasing();
                    return;
                }
            }

        }

        StopChasing();
    }

    void StartChasing()
    {
        if (!isChasing)
        {
            isChasing = true;
            patrolScript.enabled = false;
            agent.speed = chaseSpeed;

            if (!hasPlayedAlert && alertAudio != null)
            {
                alertAudio.Play();
                hasPlayedAlert = true;
            }
        }

        agent.SetDestination(player.position);
    }

    void StopChasing()
    {
        if (isChasing)
        {
            isChasing = false;
            patrolScript.enabled = true;
            hasPlayedAlert = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Vector3 forward = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-fieldOfViewAngle * 0.5f, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(fieldOfViewAngle * 0.5f, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * forward * detectionRadius;
        Vector3 rightRayDirection = rightRayRotation * forward * detectionRadius;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftRayDirection);
        Gizmos.DrawRay(transform.position, rightRayDirection);
    }
}
