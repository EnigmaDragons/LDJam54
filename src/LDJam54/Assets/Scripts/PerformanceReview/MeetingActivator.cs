
public class MeetingActivator : OnMessage<WorkdayEnded>
{
    public GameConfig cfg;
    
    protected override void Execute(WorkdayEnded msg)
    {
        PerformanceEvaluator.Evaluate(cfg);
        CurrentGameState.Update(g => g.MeetingTime = MeetingTime.Evening);
        Message.Publish(new StartPerformanceMeetingRequested());
    }
}
