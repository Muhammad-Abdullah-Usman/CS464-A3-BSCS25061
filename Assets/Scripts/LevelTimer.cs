using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float timeLimit = 35f;

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
        ShowTime();

        if (remaining <= 0f)
        {
            expired = true;
            GameManager.Instance.TimeExpired();
        }
    }

    void ShowTime()
    {
        timerText.text = $"Time: {Mathf.CeilToInt(remaining)}";
    }
}