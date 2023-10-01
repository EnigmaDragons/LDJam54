using UnityEngine;

public class WorktimeController : OnMessage<StartWorkdayRequested, WorkdayEnded, EndWorkdayRequested>
{
    [SerializeField] private GameConfig config;
    
    private bool _worktimeIsActive;
    private float _elapsedInCurrentDay;
    
    protected override void Execute(StartWorkdayRequested msg)
    {
        WorkdayState.InitWorkday();
        _worktimeIsActive = true;
        _elapsedInCurrentDay = 0f;
    }

    protected override void Execute(WorkdayEnded msg)
    {
        _worktimeIsActive = false;
        CurrentGameState.Instance.UpdateState(g => g.MeetingTime = MeetingTime.Evening);
    }

    protected override void Execute(EndWorkdayRequested msg)
    {
        _worktimeIsActive = false;
        WorkdayState.SetTotalWorkdayMinutes((WorkdayConfig.END_OF_DAY - WorkdayConfig.START_OF_DAY) * 60);
    }

    private void FixedUpdate()
    {
        if (!_worktimeIsActive) return;
        
        _elapsedInCurrentDay += Time.fixedDeltaTime;
        var totalMinutes = (_elapsedInCurrentDay / WorkdayConfig.NUM_SECONDS_PER_WORK_MINUTE) * config.ClockSpeedFactor;
        WorkdayState.SetTotalWorkdayMinutes((int)totalMinutes);
    }
}
