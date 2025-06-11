using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign the UI TextMeshProUGUI element in the inspector
    public List<MonoBehaviour> scriptsToDeactivate; // Assign the scripts to deactivate in the inspector
    public GameObject objectToDeactivate; // Assign the GameObject to deactivate when the timer hits zero

    private float timerDuration = 60f;
    private bool isTimerRunning = false;

    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            StartCoroutine(TimerCoroutine());
        }
    }

    private IEnumerator TimerCoroutine()
    {
        isTimerRunning = true;
        float remainingTime = timerDuration;

        while (remainingTime > 0)
        {
            timerText.text = "T: " + Mathf.Ceil(remainingTime).ToString();
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "T: 0";
        DeactivateScripts();

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false); // Deactivate the GameObject when the timer hits zero
        }

        isTimerRunning = false;
    }

    private void DeactivateScripts()
    {
        foreach (var script in scriptsToDeactivate)
        {
            if (script != null)
            {
                script.enabled = false; // Deactivate each script
            }
        }
    }
}
