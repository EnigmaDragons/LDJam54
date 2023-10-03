using UnityEngine;

public class SetCursorStateOnEnable : MonoBehaviour
{
    [SerializeField] private bool shouldBeVisible = true;

    private void OnEnable() => MouseState.SetCursorVisible(shouldBeVisible);
}
