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

    // Performance Review
    public List<Worker> Coworkers = new() { new Worker() { Name = "23472" } };
    public float ClockSpeedFactor = 1f;
    public KPI[] ActiveKPIs = { KPI.BoxesShipped };
    public int[] FlowActiveKPIs = { 0 };
    public SerializableDictionary<KPI, CoworkerAverage> CoworkerStandardPerformance = new() {
        { KPI.BoxesShelvedCorrectly, new CoworkerAverage() },
        { KPI.BoxesShipped, new CoworkerAverage { BaseKPI = 20, SkillBonus = 1, ExploitBaseKpi = 50, ExploitSkillBonus = 3 } },
        { KPI.BoxesUnsorted, new CoworkerAverage() },
        //{ KPI.BoxesUnloaded, new CoworkerAverage() },
    };
    public SerializableDictionary<KPI, int> KPIScoring = new()
    {
        { KPI.BoxesShelvedCorrectly, 20 },
        { KPI.BoxesShipped, 20 },
        { KPI.BoxesUnsorted, 20 },
        //{ KPI.BoxesUnloaded, 20 },
    };
    public SerializableDictionary<KPI, int> KPIs = new() {
        { KPI.BoxesShelvedCorrectly, 0 },
        { KPI.BoxesShipped, 0 },
        { KPI.BoxesUnsorted, 0 },
        //{ KPI.BoxesUnloaded, 0 },
    };
    public PerformanceReview PerformanceReview = new PerformanceReview();
    
    // Days
    public int CurrentDayNumber = 1;
    public int TotalDays = 1;
    public MeetingTime MeetingTime = MeetingTime.Morning;

    // Cutscene Stuff - Probably Not Needed
    public SerializableDictionary<string, bool> CutsceneStoryStates = new();
}
