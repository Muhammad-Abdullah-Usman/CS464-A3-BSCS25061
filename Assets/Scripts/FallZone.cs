using UnityEngine;

public class FallZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Health health = other.GetComponent<Health>();
        if (health != null)
            health.Kill();
    }
}