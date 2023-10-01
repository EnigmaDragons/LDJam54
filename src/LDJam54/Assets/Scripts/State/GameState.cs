using System;
using System.Collections.Generic;

[Serializable]
public sealed class GameState
{
    // Should consist of only serializable primitives.
    // Any logic or non-trivial data should be enriched in CurrentGameState.
    // Except for Save/Load Systems, everything should use CurrentGameState,
    // instead of this pure data structure.

    // All enums used in this class should have specified integer values.
    // This is necessary to preserve backwards save compatibility.

    public string PlayerID = "464947";
    public List<Worker> Coworkers = new() { new Worker() { Name = "23472" } };
    public float ClockSpeedFactor = 1f;
    public KPI[] ActiveKPIs = { KPI.BoxShipped };
    public int[] FlowActiveKPIs = { 0 };
    public SerializableDictionary<KPI, CoworkerAverage> CoworkerStandardPerformance = new() {
        { KPI.PlacedCorrectly, new CoworkerAverage() },
        { KPI.BoxShipped, new CoworkerAverage { BaseKPI = 20, SkillBonus = 1, ExploitBaseKpi = 50, ExploitSkillBonus = 3 } },
        { KPI.BoxUnsorted, new CoworkerAverage() },
    };
    public SerializableDictionary<KPI, int> KPIScoring = new()
    {
        { KPI.PlacedCorrectly, 20 },
        { KPI.BoxShipped, 20 },
        { KPI.BoxUnsorted, 20 },
    };
    public SerializableDictionary<KPI, int> KPIs = new() {
        { KPI.PlacedCorrectly, 0 },
        { KPI.BoxShipped, 0 },
        { KPI.BoxUnsorted, 0 },
    };
    public PerformanceReview PerformanceReview = new PerformanceReview();
    public SerializableDictionary<string, bool> CutsceneStoryStates = new();

    public int CurrentDayNumber = 1;
    public int TotalDays = 1;
}


public enum KPI
{
    PlacedCorrectly = 0,
    BoxShipped = 1,
    BoxUnsorted = 2,
}