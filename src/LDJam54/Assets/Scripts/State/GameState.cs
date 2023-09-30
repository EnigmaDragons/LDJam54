using System;

[Serializable]
public sealed class GameState
{
    // Should consist of only serializable primitives.
    // Any logic or non-trivial data should be enriched in CurrentGameState.
    // Except for Save/Load Systems, everything should use CurrentGameState,
    // instead of this pure data structure.

    // All enums used in this class should have specified integer values.
    // This is necessary to preserve backwards save compatibility.
    public string PlayerName = "John Doe";
    public string[] Coworkers = new string[] { "Bruce", "Jack" };
    public string[] ActiveKPIs = new string[] { "PlacedCorrectly", "TestKPI" };
    public SerializableDictionary<string, int> KPIs = new() {
        { "PlacedCorrectly", 0 },
        { "TestKPI", 11}
    };
    public PerformanceReview PerformanceReview = new PerformanceReview();
    public SerializableDictionary<string, bool> CutsceneStoryStates = new();
}
