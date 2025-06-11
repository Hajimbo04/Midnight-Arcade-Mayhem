using UnityEngine;

public class CupDetector : MonoBehaviour
{
    public AudioSource scoreSound;
    public GameObject cupParent;
    public bool destroyOnScore = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Scored!");
            if (scoreSound) scoreSound.Play();

            // 🟡 NEW: Safely add to score
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.AddScore(1); // Increase score by 1
            }

            if (destroyOnScore)
                Destroy(cupParent);
        }
    }
}
