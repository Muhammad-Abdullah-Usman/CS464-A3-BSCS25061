using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float knockback = 8f;
    [SerializeField] float knockTime = 0.25f;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Health health = other.GetComponent<Health>();

        if (health == null || !health.TakeDamage(damage))
            return;

        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player == null || health.Current <= 0)
            return;

        Vector3 away =
            other.transform.position - transform.position;
        away.y = 0f;

        if (away.sqrMagnitude < 0.001f)
            away = Vector3.back;

        player.Knockback(
            away.normalized * knockback, knockTime);
    }
}