
public class NextDayHandler : OnMessage<StartNextDayRequested>
{
    protected override void Execute(StartNextDayRequested msg)
    {
        if (CurrentGameState.State.PlayerIsFired)
        {
            Message.Publish(new WasFired());
            return;
        }
        
        CurrentGameState.Update(g =>
        {
            g.CurrentDayNumber++;
            g.MeetingTime = MeetingTime.Morning;
        });
        
        Message.Publish(new DayChanged());
    }
}
