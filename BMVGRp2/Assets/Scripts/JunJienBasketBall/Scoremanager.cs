using UnityEngine;
using TMPro;

public class Scoremanager : MonoBehaviour
{
    public int currentScore = 0;               // Use this instead of "score"
    public TextMeshProUGUI scoreText;

    public void AddScore(int value)
    {
        currentScore += value;
        scoreText.text = currentScore.ToString();  // Display the current score
    }
}
