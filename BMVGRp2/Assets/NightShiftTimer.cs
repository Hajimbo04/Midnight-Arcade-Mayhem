using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NightShiftTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;   // UI reference
    private const float realSecondsPerHour = 20f;        // 1 real‑min == 1 game‑hour
    private readonly string[] clock = { "12AM", "1AM", "2AM",
                                        "3AM", "4AM", "5AM", "6AM" };

    private float t;          // accumulates real seconds
    private int hourIndex;    // 0 == 12 AM

    private void Start() => UpdateDisplay();

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= realSecondsPerHour)
        {
            t -= realSecondsPerHour;
            hourIndex++;
            if (hourIndex == 6)                     // reached 6 AM
                SceneManager.LoadScene("GameWin");  // done!
            else
                UpdateDisplay();
        }
    }

    private void UpdateDisplay() => timerText.text = clock[hourIndex];
}
