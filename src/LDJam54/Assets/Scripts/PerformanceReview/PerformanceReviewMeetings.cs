
public class PerformanceReviewMeetings : OnMessage<WorkdayEnded>
{
    protected override void Execute(WorkdayEnded msg)
    {
        Message.Publish(new StartPerformanceMeetingRequested());
    }
}
