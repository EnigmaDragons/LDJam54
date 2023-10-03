using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class OnPlayerEnterStartMeeting : PlayerTrigger
{
    public GameConfig cfg;
    public EventReference youAreFiredSpeech;

    private HashSet<int> _eveningMeetingSpeechesPlayed = new ();
    private HashSet<int> _morningMeetingStarted = new ();

    private void ClearHistory()
    {
        _eveningMeetingSpeechesPlayed.Clear();
        _morningMeetingStarted.Clear();
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
            TriggerMorningMeeting();
        }
        else
        {
            TriggerEveningMeeting();
        }
    }

    private void TriggerMorningMeeting()
    {
        if (_morningMeetingStarted.Contains(CurrentGameState.State.CurrentDayNumber))
            return;
        
        _morningMeetingStarted.Add(CurrentGameState.State.CurrentDayNumber);
        Message.Publish(new StartKpiMeetingRequested());
    }

    private void TriggerEveningMeeting()
    {
        if (_eveningMeetingSpeechesPlayed.Contains(CurrentGameState.State.CurrentDayNumber))
            return;
        
        _eveningMeetingSpeechesPlayed.Add(CurrentGameState.State.CurrentDayNumber);
        PerformanceEvaluator.Evaluate(cfg);
        CurrentGameState.Update(g => g.MeetingTime = MeetingTime.Evening);
        var gs = CurrentGameState.State;
        var firedId = gs.PerformanceReview.EliminatedPerson;
        var dayFiringSpeech = cfg.CurrentDayConfig.AfternoonSpeech;
        var speech = firedId.Equals(gs.PlayerID) ? youAreFiredSpeech : dayFiringSpeech;
        Log.Info($"Evening meeting speech: {speech}");
        Debug.Log("Eliminated: " + gs.PerformanceReview.EliminatedPerson);
        if (!firedId.Equals(gs.PlayerID))
            CurrentGameState.FireCoworker(firedId);
        if (!speech.IsNull)
            Message.Publish(new PlayMeetingBossMonologue(speech));
        Message.Publish(new StartPerformanceMeetingRequested());
    }
}
