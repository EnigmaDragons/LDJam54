using System.Linq;
using UnityEngine;

public class ChangeDayConfigs : OnMessage<DayChanged>
{
    [SerializeField] private GameConfig gameConfig;

    protected override void Execute(DayChanged msg)
    {
        Debug.Log("Changing Day Configs. New Day: " + CurrentGameState.State.CurrentDayNumber);
        CurrentGameState.Instance.UpdateState(s =>
        {
            var day = gameConfig.Days[s.CurrentDayNumber - 1];
            s.Coworkers = gameConfig.Coworkers.Skip(s.CurrentDayNumber - 1).ToList();
            s.PlayerIsFired = false;
            s.ResetKpi();
            s.ActiveKPIs = day.ActiveKPIs;
            s.CoworkerScores = day.CoworkerScores;
            s.ClockSpeedFactor = day.ClockSpeedFactor;
            s.BoxSpawnInterval = day.BoxSpawnInterval;
        });
    }
}
