using UnityEngine;

[CreateAssetMenu(menuName = "EventPublisher")]
public class EventPublisher : ScriptableObject
{
    public static void PublishStartWorkdayRequested() => Message.Publish(new StartWorkdayRequested());
    public static void PublishEndWorkdayRequested() => Message.Publish(new EndWorkdayRequested());
    public static void PublishWasFired() => Message.Publish(new WasFired());
}
