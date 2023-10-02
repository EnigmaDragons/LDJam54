using UnityEngine;
using UnityEngine.Events;

public class OnInteractAction : MonoBehaviour
{
    [SerializeField] private UnityEvent action;
    
    public void Update()
    {
        if (ControllerChecker.IsA() || ControllerChecker.IsB() || ControllerChecker.IsX() || ControllerChecker.IsY() || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            action.Invoke();
    }

    public void Interact() => action.Invoke();
}
