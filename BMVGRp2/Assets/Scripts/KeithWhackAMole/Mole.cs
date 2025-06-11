using UnityEngine;
using TMPro;

public class Mole : MonoBehaviour
{
    public float riseDuration = 0.5f;
    public float downDuration = 0.5f;
    public float riseHeight = 0.15f;
    public TextMeshProUGUI scoreText;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isRising = false;

    public Vector3 InitialPosition
    {
        get { return initialPosition; }
    }

    void Start()
    {
        if (ScoreManagerKeith.Instance != null)
        {
            ScoreManagerKeith.Instance.ResetScore(); // Reset score to 0 at the start
        }
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * riseHeight;
        StartCoroutine(RiseAndFallCycle());
    }

    public System.Collections.IEnumerator RiseAndFallCycle()
    {
        while (true)
        {
            yield return StartCoroutine(Rise());
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
            yield return StartCoroutine(GoDown());
            yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        }
    }

    private System.Collections.IEnumerator Rise()
    {
        isRising = true;
        float elapsedTime = 0.0f;

        while (elapsedTime < riseDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / riseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isRising = false;
    }

    private System.Collections.IEnumerator GoDown()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < downDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / downDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            StopAllCoroutines();
            StartCoroutine(GoDownAndRestartCycle());
            IncreaseScore();

            if (SoundEffectManager.Instance != null)
            {
                SoundEffectManager.Instance.PlayPlayerHitMoleSound();
            }
        }
    }

    private System.Collections.IEnumerator GoDownAndRestartCycle()
    {
        yield return StartCoroutine(GoDown());
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(RiseAndFallCycle());
    }

    private void IncreaseScore()
    {
        if (ScoreManagerKeith.Instance != null)
        {
            ScoreManagerKeith.Instance.IncreaseScore();
        }
    }
}
