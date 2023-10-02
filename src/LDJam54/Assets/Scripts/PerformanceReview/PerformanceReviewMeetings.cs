
public class PerformanceReviewMeetings : OnMessage<WorkdayEnded>
{
    protected override void Execute(WorkdayEnded msg)
    {
        PerformanceEvaluator.Evaluate();
        CurrentGameState.Update(g => g.MeetingTime = MeetingTime.Evening);
        Message.Publish(new StartPerformanceMeetingRequested());
    }
}
