using System;
using System.Collections.Generic;

[Serializable]
public sealed class GameState
{
    public static SerializableDictionary<KPI, int> EmptyKpis() => new()
    {
        { KPI.BoxesShelvedCorrectly, 20 },
        { KPI.BoxesShipped, 20 },
        { KPI.BoxesUnsorted, 20 },
        { KPI.BoxesUnloaded, 20 },
        { KPI.BoxShippingMistakes, 20 },
    };
    
    // Should consist of only serializable primitives.
    // Any logic or non-trivial data should be enriched in CurrentGameState.
    // Except for Save/Load Systems, everything should use CurrentGameState,
    // instead of this pure data structure.

    // All enums used in this class should have specified integer values.
    // This is necessary to preserve backwards save compatibility.

    public string PlayerID = "464947";
    public bool PlayerIsFired = false;

    public float ClockSpeedFactor = 1f;
    public float BoxSpawnInterval = 1.5f;
    // Performance Review
    public List<string> Coworkers = new List<string>() { "38295" };
    public int[] CoworkerScores = new int[] { 20 };
    public KPI[] ActiveKPIs = { KPI.BoxesShipped };
    public SerializableDictionary<KPI, int> KPIScoring = EmptyKpis();
    public SerializableDictionary<KPI, int> KPIs = new() {
        { KPI.BoxesShelvedCorrectly, 0 },
        { KPI.BoxesShipped, 0 },
        { KPI.BoxesUnsorted, 0 },
        { KPI.BoxesUnloaded, 0 },
        { KPI.BoxShippingMistakes, 0 },
    };
    public PerformanceReview PerformanceReview = new PerformanceReview();
    
    // Days
    public int CurrentDayNumber = 1;
    public int TotalDays = 1;
    public bool CurrentWorkdayEnded = false;
    public MeetingTime MeetingTime = MeetingTime.Morning;

    // Cutscene Stuff - Probably Not Needed
    public SerializableDictionary<string, bool> CutsceneStoryStates = new();
}
