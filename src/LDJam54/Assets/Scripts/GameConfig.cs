using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Header("Coworkers")]
    public string[] Coworkers = new string[0];

    [Header("KPI Descriptions")]
    public string BoxShippingKPIDescription;
    public string PlacedCorrectlyKPIDescription;
    public string BoxUnsortedFixedKPIDescription;
    public string BoxUnloadedKPIDescription;
    public string BoxShippingMistakesKPIDescription;

    [Header("KPI Scoring")]
    public int BoxShippingScore;
    public int PlacedCorrectlyScore;
    public int BoxUnsortedFixedScore;
    public int BoxUnloadedScore;
    public int BoxShippingMistakesScore;

    [Header("Player Movement")] 
    public float PlayerWalkSpeed = 5f;
    public float PlayerRunSpeed = 10f;
    public float PlayerJumpForce = 2f;

    [Header("Days")]
    public int StartingDay = 1;
    public int TotalPlayableDays = 1;
    public DayConfig[] Days;

    public DayConfig CurrentDayConfig => Days[CurrentGameState.State.CurrentDayNumber-1];
    
    public SerializableDictionary<KPI, int> KpiScoring => new()
    {
        { KPI.BoxesShelvedCorrectly, PlacedCorrectlyScore },
        { KPI.BoxesShipped, BoxShippingScore },
        { KPI.BoxesTidied, BoxUnsortedFixedScore },
        { KPI.BoxesUnloaded, BoxUnloadedScore },
        { KPI.BoxShippingMistakes, BoxShippingMistakesScore },
    };
}
