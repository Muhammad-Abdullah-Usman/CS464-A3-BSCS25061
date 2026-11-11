using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    [SerializeField] float multiplier = 1.5f;
    [SerializeField] float duration = 6f;

    bool collected;

    void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        if (!GameManager.Instance.IsPlaying)
            return;

        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player == null)
            return;

        collected = true;
        player.ApplySpeedBoost(multiplier, duration);
        Destroy(gameObject);
    }
}