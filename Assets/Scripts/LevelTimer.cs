using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float timeLimit = 35f;
    [SerializeField] bool survivalMode;

    float remaining;
    bool expired;

    void Start()
    {
        remaining = timeLimit;
        ShowTime();
    }

    void Update()
    {
        if (expired || Time.timeScale == 0f)
            return;

        remaining = Mathf.Max(0f, remaining - Time.deltaTime);

        if (remaining <= 0f)
        {
            expired = true;

            if (survivalMode)
                GameManager.Instance.SurvivalFinished();
            else
                GameManager.Instance.TimeExpired();
        }

        ShowTime();
    }

    void ShowTime()
    {
        if (expired && survivalMode)
        {
            timerText.text = "Survival complete";
            return;
        }

        string label = survivalMode ? "Survive" : "Time";
        timerText.text = $"{label}: {Mathf.CeilToInt(remaining)}";
    }
}