public class OnPlayerEnterStartWorkday : PlayerTrigger
{
    protected override void OnTriggered() => Message.Publish(new StartWorkdayRequested());
}
