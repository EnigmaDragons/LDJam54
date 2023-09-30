using System;
using UnityEngine.Serialization;

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
    public string[] CoworkerIds = { "123456", "234567", "345678", "456789", "567890", "678901" };
    public KPI[] ActiveKPIs = { KPI.PlacedCorrectly, KPI.BoxShipped };
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