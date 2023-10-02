using UnityEngine;

public abstract class PlayerTrigger : MonoBehaviour
{
    public bool CanTriggerRepeatedly = false;
    
    private bool _triggered;

    private void Awake() => _triggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
            return;
        
        if (other.CompareTag("Player"))
        {
            OnTriggered();
            if (!CanTriggerRepeatedly)
            {
                _triggered = true;
                enabled = false;
            }
        }
    }

    protected abstract void OnTriggered();
}