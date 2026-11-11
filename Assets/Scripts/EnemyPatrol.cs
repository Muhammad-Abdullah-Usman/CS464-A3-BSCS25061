using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyPatrol : MonoBehaviour
{
    enum GuardState { Patrol, Chase, Return }

    [SerializeField] Transform[] points;
    [SerializeField] float patrolSpeed = 2f;
    [SerializeField] float chaseSpeed = 3.5f;
    [SerializeField] float chaseRange = 4f;
    [SerializeField] float giveUpRange = 6f;
    [SerializeField] float arriveDistance = 0.15f;
    [SerializeField] GuardState state;

    Rigidbody body;
    Transform player;
    int index;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Start()
    {
        PlayerController found =
            FindAnyObjectByType<PlayerController>();

        if (found != null)
            player = found.transform;
    }

    void FixedUpdate()
    {
        if (player == null || points == null ||
            points.Length == 0)
            return;

        if (!GameManager.Instance.IsPlaying) return;

        float distance =
            Vector3.Distance(body.position, player.position);

        if (state == GuardState.Patrol && distance < chaseRange)
            state = GuardState.Chase;
        else if (state == GuardState.Chase && distance > giveUpRange)
            state = GuardState.Return;

        Vector3 target = state == GuardState.Chase
            ? player.position
            : points[index].position;

        target.y = body.position.y;

        float speed = state == GuardState.Chase
            ? chaseSpeed
            : patrolSpeed;

        body.MovePosition(Vector3.MoveTowards(
            body.position, target,
            speed * Time.fixedDeltaTime));

        if (state != GuardState.Chase &&
            Vector3.Distance(body.position, target) < arriveDistance)
        {
            if (state == GuardState.Return)
                state = GuardState.Patrol;
            else
                index = (index + 1) % points.Length;
        }
    }
}