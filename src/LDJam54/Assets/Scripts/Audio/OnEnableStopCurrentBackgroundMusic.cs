using UnityEngine;

public class OnEnableStopCurrentBackgroundMusic : MonoBehaviour
{
    private void OnEnable()
    {
        Message.Publish(new StopCurrentBackgroundMusic());
    }
}
