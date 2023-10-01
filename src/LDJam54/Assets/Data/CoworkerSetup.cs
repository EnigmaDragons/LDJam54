using UnityEngine;

public class CoworkerSetup : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    
    public void Start()
    {
        CurrentGameState.State.Coworkers = gameConfig.Coworkers;
        CurrentGameState.State.CoworkerStandardPerformance = new() {
            { KPI.PlacedCorrectly, gameConfig.PlacedCorrectlyAverage },
            { KPI.BoxShipped, gameConfig.BoxShippedAverage },
            { KPI.BoxUnsorted, gameConfig.BoxUnsortedFixedAverage }
        };
    }
}
