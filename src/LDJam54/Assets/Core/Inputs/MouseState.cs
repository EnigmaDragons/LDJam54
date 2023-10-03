using UnityEngine;

public static class MouseState
{
    private static int _numCursorVisibleLocks = 0;

    public static void AddCursorVisibleLock()
    {
        _numCursorVisibleLocks++;
        ShowMouseCursor();
    }

    public static void RemoveCursorVisibleLock()
    {
        _numCursorVisibleLocks--;
        HideMouseCursor();
    }
    
    public static void HideMouseCursor() => SetCursorVisible(false);
    public static void ShowMouseCursor() => SetCursorVisible(true);

    public static void SetCursorVisible(bool shouldBeVisible)
    {
        var finalShouldBeVisible = shouldBeVisible || _numCursorVisibleLocks > 0;
        
        Cursor.lockState = !finalShouldBeVisible ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = finalShouldBeVisible;
        Debug.Log($"Cursor should be shown: {finalShouldBeVisible}. LockState: {Cursor.lockState}. Visible: {Cursor.visible}");
    }
}
