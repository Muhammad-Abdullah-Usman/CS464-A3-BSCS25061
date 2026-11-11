using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float upwardSpeed = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!GameManager.Instance.IsPlaying) return;

        Rigidbody body = other.attachedRigidbody;
        if (body == null) return;

        Vector3 velocity = body.linearVelocity;
        body.linearVelocity =
            new Vector3(velocity.x, 0f, velocity.z);

        body.AddForce(
            Vector3.up * upwardSpeed,
            ForceMode.VelocityChange);
    }
}