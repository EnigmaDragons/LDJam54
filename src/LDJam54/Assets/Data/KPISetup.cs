using UnityEngine;

public class KPISetup : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;

    public void Start()
    {
        CurrentGameState.State.KPIDescriptions = new()
        {
            { KPI.BoxesShelvedCorrectly, gameConfig.PlacedCorrectlyKPIDescription },
            { KPI.BoxesShipped, gameConfig.BoxShippingKPIDescription },
            { KPI.BoxesTidied, gameConfig.BoxUnsortedFixedKPIDescription },
            { KPI.BoxesUnloaded, gameConfig.BoxUnloadedKPIDescription },
            { KPI.BoxShippingMistakes, gameConfig.BoxShippingMistakesKPIDescription },
        };
    }
}
