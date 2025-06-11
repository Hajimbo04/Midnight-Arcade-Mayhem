using UnityEngine;
using TMPro;

public class ScoreManagerKeith : MonoBehaviour
{
    public static ScoreManagerKeith Instance { get; private set; }
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int IncreaseScore()
    {
        score++;
        UpdateScoreText();
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
    }
}
