using UnityEngine;
using UnityEngine.UI;
using TMPro; // Only if you're using TextMeshPro

public class Timer : MonoBehaviour
{
    public float totalTime = 120f; // 3 minutes
    private float currentTime;

    public TMP_Text timerText; // for normal UI Text
    // public TMP_Text timerText; // if you're using TextMeshPro

    private bool isRunning = true;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        if (isRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            TimerEnd();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void TimerEnd()
    {
        Debug.Log("Timer ended!");
        // Disable throwing or show results here if needed
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        isRunning = true;
    }
}
    