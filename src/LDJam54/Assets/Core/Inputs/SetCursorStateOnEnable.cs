using UnityEngine;

public class SetCursorStateOnEnable : MonoBehaviour
{
    [SerializeField] private bool shouldBeVisible = true;

    private void OnEnable()
    {        
        Cursor.lockState = !shouldBeVisible ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = shouldBeVisible;
    }
}
