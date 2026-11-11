using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text livesText;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] int targetScore = 5;
    [SerializeField] int startingLives = 3;

    int score;
    int lives;
    bool gameEnded;
    bool respawning;

    Health playerHealth;
    Rigidbody playerBody;
    Vector3 spawnPosition;

    public bool IsPlaying => !gameEnded && !respawning;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        lives = startingLives;
    }

    void Start()
    {
        playerHealth = FindAnyObjectByType<Health>();
        playerBody = playerHealth.GetComponent<Rigidbody>();
        spawnPosition = playerBody.position;

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
    }

    public void AddScore(int amount)
    {
        if (!IsPlaying) return;

        score += amount;
        UpdateUI();

        if (score >= targetScore)
            EndGame(winPanel);
    }

    public void UpdateUI()
    {
        scoreText.text = $"Coins: {score} / {targetScore}";
        healthText.text =
            $"Hearts: {playerHealth.Current} / {playerHealth.MaxHealth}";
        livesText.text = $"Lives: {lives}";
    }

    public void PlayerDied()
    {
        if (!IsPlaying) return;

        lives--;
        UpdateUI();

        if (lives <= 0)
            EndGame(losePanel);
        else
            StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        respawning = true;
        yield return new WaitForSeconds(0.6f);

        playerBody.position = spawnPosition;
        playerBody.linearVelocity = Vector3.zero;
        playerBody.angularVelocity = Vector3.zero;
        playerHealth.Restore(1.5f);

        respawning = false;
        UpdateUI();
    }

    void EndGame(GameObject panel)
    {
        gameEnded = true;
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next >= SceneManager.sceneCountInBuildSettings)
            next = 0;

        Time.timeScale = 1f;
        SceneManager.LoadScene(next);
    }
}