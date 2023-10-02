using UnityEngine;

[CreateAssetMenu(menuName = "EventPublisher")]
public class EventPublisher : ScriptableObject
{
    public static void PublishStartWorkdayRequested() => Message.Publish(new StartWorkdayRequested());
    public static void PublishEndWorkdayRequested() => Message.Publish(new EndWorkdayRequested());
    public static void PublishWasFired() => Message.Publish(new WasFired());
    public static void PublishRestartDayRequested() => Message.Publish(new RestartDayRequested());
    public static void PublishInstaWinDayRequested() => Message.Publish(new InstaWinDayRequested());
    public static void SetCurrentDay(int day) 
    {
        CurrentGameState.Instance.UpdateState(g => g.CurrentDayNumber = day);
        Message.Publish(new DayChanged());
    }
}
