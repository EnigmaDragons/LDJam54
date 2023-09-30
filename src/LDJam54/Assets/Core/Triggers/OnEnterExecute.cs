using UnityEngine;
using UnityEngine.Events;

public class OnEnterExecute : MonoBehaviour
{
    public UnityEvent action;
    public string tagFilter;
    public bool canTriggerMoreThanOnce = false;
    
    private bool _hasTriggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered) return;
        
        if (string.IsNullOrWhiteSpace(tagFilter) || other.CompareTag(tagFilter))
        {
            if (!canTriggerMoreThanOnce)
                _hasTriggered = true;
            action.Invoke();
        }
    }
}
