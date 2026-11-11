using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 20f;

    Rigidbody body;
    Vector3 input;
    float noControlUntil;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        input = Vector3.ClampMagnitude(
            new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0f,
                Input.GetAxisRaw("Vertical")),
            1f);
    }

    void FixedUpdate()
    {
        if (Time.time < noControlUntil) return;

        if (GameManager.Instance != null &&
            !GameManager.Instance.IsPlaying)
            return;

        body.AddForce(input * moveForce, ForceMode.Force);
    }

    public void Knockback(Vector3 push, float seconds)
    {
        Vector3 velocity = body.linearVelocity;
        body.linearVelocity =
            new Vector3(0f, velocity.y, 0f);

        body.AddForce(push, ForceMode.Impulse);
        noControlUntil = Time.time + seconds;
    }
}