using UnityEngine.Events;
using UnityEngine;

public class AdsLoading : MonoBehaviour
{
    [SerializeField] private UnityEvent Trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Trigger.Invoke();
        }
    }
}
