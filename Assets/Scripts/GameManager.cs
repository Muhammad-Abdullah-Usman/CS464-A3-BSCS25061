using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TMP_Text scoreText;
    [SerializeField] int targetScore = 5;
    [SerializeField] GameObject winPanel;

    int score;
    bool gameEnded;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        winPanel.SetActive(false);
        UpdateScoreText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
    }

    public void AddScore(int amount)
    {
        if (gameEnded) return;

        score += amount;
        UpdateScoreText();

        if (score >= targetScore)
            Win();
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Coins: {score} / {targetScore}";
    }

    void Win()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}