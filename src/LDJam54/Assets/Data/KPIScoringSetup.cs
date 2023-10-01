using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPIScoringSetup : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;

    public void Start()
    {
        CurrentGameState.State.KPIScoring = new()
        {
            { KPI.BoxesShelvedCorrectly, gameConfig.PlacedCorrectlyScore },
            { KPI.BoxesShipped, gameConfig.BoxShippingScore },
            { KPI.BoxesUnsorted, gameConfig.BoxUnsortedFixedScore }
        };
    }
}
