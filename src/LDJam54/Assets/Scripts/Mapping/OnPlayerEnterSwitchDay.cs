
using System;

public class OnPlayerEnterSwitchDay: PlayerTrigger
{
    private bool _waitingForDayChange;

    private void Awake()
    {
        _waitingForDayChange = false;
    }

    private void OnEnable() => Message.Subscribe<DayChanged>(_ => _waitingForDayChange = false, this);
    private void OnDisable() => Message.Unsubscribe(this);

    protected override void OnTriggered()
    {
        if (_waitingForDayChange)
            return;
        _waitingForDayChange = true;
        Message.Publish(new StartNextDayRequested());
    }
}
