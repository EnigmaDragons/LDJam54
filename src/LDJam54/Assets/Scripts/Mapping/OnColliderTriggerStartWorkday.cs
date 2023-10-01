using UnityEngine;

public class OnColliderTriggerStartWorkday : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Message.Publish(new StartWorkdayRequested());
            enabled = false;
        }
    }
}
