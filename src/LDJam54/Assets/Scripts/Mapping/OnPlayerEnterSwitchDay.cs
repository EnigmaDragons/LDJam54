
using System;

public class OnPlayerEnterSwitchDay : PlayerTrigger
{
    private bool _workdayOver;

    private void Awake()
    {
        _workdayOver = false;
    }

    private void OnEnable() => Message.Subscribe<WorkdayEnded>(_ => _workdayOver = true, this);
    private void OnDisable() => Message.Unsubscribe(this);

    protected override void OnTriggered()
    {
        if (!_workdayOver)
            return;
        _workdayOver = false;
        Message.Publish(new StartNextDayRequested());
    }
}
