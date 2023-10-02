
public class NextDayHandler : OnMessage<StartNextDayRequested, RestartDayRequested>
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
            g.KPIScoring = GameState.EmptyKpis();
        });
        
        Message.Publish(new DayChanged());
    }

    protected override void Execute(RestartDayRequested msg)
    {
        Log.Info("Restarting Day");
        CurrentGameState.Update(g =>
        {
            g.MeetingTime = MeetingTime.Morning;
            g.KPIScoring = GameState.EmptyKpis();
        });
        WorkdayFadeController.TeleportPlayerToHome();
        Message.Publish(new DayChanged());
    }
}
