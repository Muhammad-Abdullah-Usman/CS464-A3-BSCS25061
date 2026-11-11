using UnityEngine;

public class ExitGoal : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.TryExit();
    }
}