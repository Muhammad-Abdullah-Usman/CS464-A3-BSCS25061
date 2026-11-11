using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 20f;

    Rigidbody body;
    Vector3 input;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        input = Vector3.ClampMagnitude(
            new Vector3(horizontal, 0f, vertical), 1f);
    }

    void FixedUpdate()
    {
        body.AddForce(input * moveForce, ForceMode.Force);
    }
}