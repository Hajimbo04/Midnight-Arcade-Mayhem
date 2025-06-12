using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance { get; private set; }

    public GameObject startPanel;
    public GameObject scorePanel;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        scorePanel.SetActive(true);
        UpdateScore(ScoreManager.Instance.CurrentScore); // Initialize display
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore.ToString();
    }
}
