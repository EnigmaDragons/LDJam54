using UnityEngine;

public class KeepMouseCursorVisibleWhileEnabled : MonoBehaviour
{
    private void OnEnable() => MouseState.AddCursorVisibleLock();
    private void OnDisable() => MouseState.RemoveCursorVisibleLock();
}
