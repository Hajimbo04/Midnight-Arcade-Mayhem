using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NightShiftTimer : MonoBehaviour
{
    private const float realSecondsPerHour = 20f; // 20s = 1 in-game hour (adjust as needed)
    private readonly string[] clock = { "12AM", "1AM", "2AM", "3AM", "4AM", "5AM", "6AM" };

    private float t;
    private int hourIndex;

    private TextMeshProUGUI[] allClocks;

    private void Start()
    {
        // Find all TMP clocks in the scene with tag "Clock"
        GameObject[] clockObjects = GameObject.FindGameObjectsWithTag("Clock");

        allClocks = new TextMeshProUGUI[clockObjects.Length];
        for (int i = 0; i < clockObjects.Length; i++)
        {
            allClocks[i] = clockObjects[i].GetComponent<TextMeshProUGUI>();
        }

        UpdateDisplay();
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= realSecondsPerHour)
        {
            t -= realSecondsPerHour;
            hourIndex++;
            if (hourIndex == 6)
            {
                SceneManager.LoadScene("GameWin");
            }
            else
            {
                UpdateDisplay();
            }
        }
    }

    private void UpdateDisplay()
    {
        foreach (var clockText in allClocks)
        {
            if (clockText != null)
                clockText.text = clock[hourIndex];
        }
    }
}
