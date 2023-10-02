public class OnPlayerEnterStartPerformanceReview : PlayerTrigger
{
    protected override void OnTriggered() => Message.Publish(new StartPerformanceMeetingRequested());
}
