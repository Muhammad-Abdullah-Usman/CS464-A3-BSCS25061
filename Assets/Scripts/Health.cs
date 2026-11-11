using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] float iFrames = 1f;

    int current;
    float invulnerableUntil;

    public int Current => current;
    public int MaxHealth => maxHealth;

    void Awake()
    {
        current = maxHealth;
    }

    public bool TakeDamage(int amount)
    {
        if (current <= 0 ||
            Time.time < invulnerableUntil ||
            !GameManager.Instance.IsPlaying)
            return false;

        current = Mathf.Max(0, current - amount);
        invulnerableUntil = Time.time + iFrames;
        GameManager.Instance.UpdateUI();

        if (current <= 0)
            GameManager.Instance.PlayerDied();

        return true;
    }

    public void Kill()
    {
        if (current <= 0 || !GameManager.Instance.IsPlaying)
            return;

        current = 0;
        GameManager.Instance.PlayerDied();
    }

    public void Restore(float protection)
    {
        current = maxHealth;
        invulnerableUntil = Time.time + protection;
    }
}