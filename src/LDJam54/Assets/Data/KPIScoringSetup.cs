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
            { KPI.PlacedCorrectly, gameConfig.PlacedCorrectlyScore },
            { KPI.BoxShipped, gameConfig.BoxShippingScore },
            { KPI.BoxUnsorted, gameConfig.BoxUnsortedFixedScore }
        };
    }
}
