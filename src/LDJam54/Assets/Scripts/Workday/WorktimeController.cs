using UnityEngine;

public class WorktimeController : OnMessage<StartWorkdayRequested, WorkdayEnded, EndWorkdayRequested, DayChanged>
{   
    private bool _worktimeIsActive;
    private float _elapsedInCurrentDay;
    
    protected override void Execute(StartWorkdayRequested msg)
    {
        WorkdayState.InitWorkday();
        _worktimeIsActive = true;
        _elapsedInCurrentDay = 0f;
        CurrentGameState.Update(g => g.CurrentWorkdayEnded = false);
        Message.Publish(new WorkdayStarted());
    }

    protected override void Execute(WorkdayEnded msg)
    {
        _worktimeIsActive = false;
        CurrentGameState.Update(g =>
        {
            g.MeetingTime = MeetingTime.Evening;
            g.CurrentWorkdayEnded = true;
        });
    }

    protected override void Execute(EndWorkdayRequested msg)
    {
        _worktimeIsActive = false;
        WorkdayState.SetTotalWorkdayMinutes((WorkdayConfig.END_OF_DAY - WorkdayConfig.START_OF_DAY) * 60);
    }

    protected override void Execute(DayChanged msg)
    {
        WorkdayState.InitWorkday();
    }

    private void FixedUpdate()
    {
        if (!_worktimeIsActive) return;
        
        _elapsedInCurrentDay += Time.fixedDeltaTime;
        var totalMinutes = (_elapsedInCurrentDay / WorkdayConfig.NUM_SECONDS_PER_WORK_MINUTE) * CurrentGameState.State.ClockSpeedFactor;
        WorkdayState.SetTotalWorkdayMinutes((int)totalMinutes);
    }
}
