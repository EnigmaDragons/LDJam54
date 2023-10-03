using System;

[Obsolete]
public class MeetingActivator : OnMessage<WorkdayEnded>
{
    public GameConfig cfg;
    
    protected override void Execute(WorkdayEnded msg)
    {
    }
}
