using UnityEngine;
using TMPro;  // Needed for TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void AddScore(int value)
    {
        score += value;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
