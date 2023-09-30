using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OnKeyCombination : MonoBehaviour
{
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private UnityEvent action;
    [SerializeField] private bool canTriggerMoreThanOncePerPress = false;

    private void Update()
    {
        if (keys.All(Input.GetKey) && (canTriggerMoreThanOncePerPress || keys.Any(Input.GetKeyDown)))
            action.Invoke();
    }
}
