using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public Scoremanager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            scoreManager.AddScore(2);
        }
    }
}
