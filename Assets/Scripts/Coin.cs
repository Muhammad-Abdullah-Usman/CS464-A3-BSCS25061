using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;

    bool collected;

    void Update()
    {
        transform.Rotate(
            Vector3.up,
            spinSpeed * Time.deltaTime,
            Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        if (!GameManager.Instance.IsPlaying) 
            return;

        collected = true;
        GameManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}