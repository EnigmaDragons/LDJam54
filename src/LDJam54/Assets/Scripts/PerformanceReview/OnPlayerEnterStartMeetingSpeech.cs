using System.Collections.Generic;
using FMODUnity;

public class OnPlayerEnterStartMeetingSpeech : PlayerTrigger
{
    public GameConfig cfg;
    public EventReference youAreFiredSpeech;

    private HashSet<int> _eveningMeetingSpeechesPlayed = new ();
    private HashSet<int> _morningMeetingSpeechesPlayed = new ();

    private void ClearHistory()
    {
        _eveningMeetingSpeechesPlayed.Clear();
        _morningMeetingSpeechesPlayed.Clear();
    }
    
    private void OnEnable()
    {
        Message.Subscribe<RestartDayRequested>(e => ClearHistory(), this);
    }

    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    protected override void OnTriggered()
    {
        var meetingTime = CurrentGameState.State.MeetingTime;
        Log.Info($"Player entered start meeting speech trigger at {meetingTime}");
        if (meetingTime == MeetingTime.Morning)
        {
        }
        else
        {
            TriggerEveningMeeting();
        }
    }

    private void TriggerEveningMeeting()
    {
        if (_eveningMeetingSpeechesPlayed.Contains(CurrentGameState.State.CurrentDayNumber))
            return;
        
        _eveningMeetingSpeechesPlayed.Add(CurrentGameState.State.CurrentDayNumber);
        var gs = CurrentGameState.State;
        var youAreFired = gs.PlayerIsFired;
        var dayFiringSpeech = cfg.CurrentDayConfig.AfternoonSpeech;
        var speech = youAreFired ? youAreFiredSpeech : dayFiringSpeech;
        Log.Info($"Evening meeting speech: {speech}");
        if (!speech.IsNull)
            Message.Publish(new PlayMeetingBossMonologue(speech));
    }
}
