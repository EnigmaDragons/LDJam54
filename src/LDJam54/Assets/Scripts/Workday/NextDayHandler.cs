﻿using UnityEngine;

public class NextDayHandler : OnMessage<StartNextDayRequested, RestartDayRequested>
{
    [SerializeField] private float minimumSecondsDelay;

    private float _t;

    private void Awake() => _t = 0;
    
    private void Update()
    {
        if (_t <= 0)
            return;
        _t -= Time.deltaTime;
    }
    
    protected override void Execute(StartNextDayRequested msg)
    {
        if (_t > 0)
            return;
        _t = minimumSecondsDelay;
        if (CurrentGameState.State.PerformanceReview.EliminatedPerson.Equals(CurrentGameState.State.PlayerID))
        {
            Message.Publish(new WasFired());
            return;
        }
        
        CurrentGameState.Update(g =>
        {
            g.CurrentDayNumber++;
            g.MeetingTime = MeetingTime.Morning;
            g.KPIs = GameState.ZeroedKpis();
        });
        
        Message.Publish(new DayChanged());
    }

    protected override void Execute(RestartDayRequested msg)
    {
        Log.Info("Restarting Day");
        CurrentGameState.Update(g =>
        {
            g.MeetingTime = MeetingTime.Morning;
            g.KPIs = GameState.ZeroedKpis();
        });
        WorkdayFadeController.TeleportPlayerToHome();
        Message.Publish(new DayChanged());
    }
}
