using UnityEngine;

public abstract class PlayerTrigger : MonoBehaviour
{
    private bool _triggered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
            return;
        
        if (other.CompareTag("Player"))
        {
            OnTriggered();
            _triggered = true;
            enabled = false;
        }
    }

    protected abstract void OnTriggered();
}