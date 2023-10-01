
public class OnPlayerEnterStartKpiMeeting : PlayerTrigger
{
    protected override void OnTriggered() => Message.Publish(new StartKpiMeetingRequested());
}
