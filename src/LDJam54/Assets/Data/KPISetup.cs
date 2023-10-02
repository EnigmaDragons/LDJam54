using UnityEngine;

public class KPISetup : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;

    public void Start()
    {
        CurrentGameState.State.KPIScoring = new()
        {
            { KPI.BoxesShelvedCorrectly, gameConfig.PlacedCorrectlyScore },
            { KPI.BoxesShipped, gameConfig.BoxShippingScore },
            { KPI.BoxesUnsorted, gameConfig.BoxUnsortedFixedScore },
            { KPI.BoxesUnloaded, gameConfig.BoxUnloadedScore },
            { KPI.BoxShippingMistakes, gameConfig.BoxShippingMistakesScore },
        };

        CurrentGameState.State.KPIDescriptions = new()
        {
            { KPI.BoxesShelvedCorrectly, gameConfig.PlacedCorrectlyKPIDescription },
            { KPI.BoxesShipped, gameConfig.BoxShippingKPIDescription },
            { KPI.BoxesUnsorted, gameConfig.BoxUnsortedFixedKPIDescription },
            { KPI.BoxesUnloaded, gameConfig.BoxUnloadedKPIDescription },
            { KPI.BoxShippingMistakes, gameConfig.BoxShippingMistakesKPIDescription },
        };
    }
}
