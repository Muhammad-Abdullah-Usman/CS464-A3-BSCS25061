using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Crusher : MonoBehaviour
{
    [SerializeField] float startDelay;
    [SerializeField] float warningTime = 0.8f;

    Rigidbody body;
    Collider hitbox;
    Material surface;
    Vector3 raised;
    Vector3 lowered;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        hitbox = GetComponent<Collider>();
        surface = GetComponent<Renderer>().material;

        raised = body.position;
        lowered = new Vector3(raised.x, 0.45f, raised.z);

        hitbox.enabled = false;
        surface.color = Color.gray;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            surface.color = Color.gray;
            yield return new WaitForSeconds(1.2f);

            surface.color = Color.yellow;
            yield return new WaitForSeconds(warningTime);

            surface.color = Color.red;
            hitbox.enabled = true;

            yield return MoveTo(lowered, 0.25f);
            yield return new WaitForSeconds(0.6f);

            hitbox.enabled = false;
            yield return MoveTo(raised, 0.5f);
        }
    }

    IEnumerator MoveTo(Vector3 destination, float duration)
    {
        Vector3 origin = body.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            yield return new WaitForFixedUpdate();

            elapsed += Time.fixedDeltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            body.MovePosition(Vector3.Lerp(origin, destination, progress));
        }
    }
}