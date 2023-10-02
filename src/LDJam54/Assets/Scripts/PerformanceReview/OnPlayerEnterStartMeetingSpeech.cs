
public class OnPlayerEnterStartMeetingSpeech : PlayerTrigger
{
    public GameConfig cfg;
    
    protected override void OnTriggered()
    {
        if (CurrentGameState.State.PlayerIsFired)
            return;

        var dayConfig = cfg.CurrentDayConfig;
        var meetingTime = CurrentGameState.State.MeetingTime;

        if (meetingTime == MeetingTime.Evening && !dayConfig.AfternoonSpeech.IsNull)
        {
            Message.Publish(new PlayMeetingBossMonologue(dayConfig.AfternoonSpeech));
        }
    }
}
