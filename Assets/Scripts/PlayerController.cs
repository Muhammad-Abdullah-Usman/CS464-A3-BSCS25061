using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 20f;
    [SerializeField] TMP_Text boostText;

    Rigidbody body;
    Vector3 input;
    float noControlUntil;
    float boostUntil;
    float boostMultiplier = 1f;

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

        if (boostText != null)
        {
            float remaining = Mathf.Max(0f, boostUntil - Time.time);

            boostText.text = remaining > 0f
                ? $"Speed boost: {remaining:0.0}s"
                : "Speed: normal";
        }
    }

    void FixedUpdate()
    {
        if (Time.time < noControlUntil)
            return;

        if (GameManager.Instance != null &&
            !GameManager.Instance.IsPlaying)
            return;

        float multiplier = Time.time < boostUntil
            ? boostMultiplier
            : 1f;

        body.AddForce(
            input * moveForce * multiplier,
            ForceMode.Force);
    }

    public void Knockback(Vector3 push, float seconds)
    {
        Vector3 velocity = body.linearVelocity;
        body.linearVelocity = new Vector3(0f, velocity.y, 0f);

        body.AddForce(push, ForceMode.Impulse);
        noControlUntil = Time.time + seconds;
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        boostMultiplier = multiplier;
        boostUntil = Time.time + duration;
    }
}