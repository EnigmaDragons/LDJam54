
public class OnPlayerEnterSwitchDay: PlayerTrigger
{
    protected override void OnTriggered() => Message.Publish(new StartNextDayRequested());
}
