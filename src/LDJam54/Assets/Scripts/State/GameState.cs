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
    public KPI[] ActiveKPIs = { KPI.BoxShipped };
    public SerializableDictionary<KPI, CoworkerAverage> CoworkerStandardPerformance = new() {
        { KPI.PlacedCorrectly, new CoworkerAverage() },
        { KPI.BoxShipped, new CoworkerAverage { BaseKPI = 20, SkillBonus = 1, ExploitMultiplier = 1.25f } },
    };
    public SerializableDictionary<KPI, int> KPIs = new() {
        { KPI.PlacedCorrectly, 0 },
        { KPI.BoxShipped, 0 },
    };
    public PerformanceReview PerformanceReview = new PerformanceReview();
    public SerializableDictionary<string, bool> CutsceneStoryStates = new();
}


public enum KPI
{
    PlacedCorrectly = 0,
    BoxShipped = 1,
}