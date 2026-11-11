using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TMP_Text scoreText;
    [SerializeField] int targetScore = 5;

    int score;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Coins: {score} / {targetScore}";
    }
}