using UnityEngine;

public class CoworkerSetup : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    
    public void Start()
    {
        CurrentGameState.State.Coworkers = gameConfig.Coworkers;
        CurrentGameState.State.CoworkerStandardPerformance = new() {
            { KPI.PlacedCorrectly, new CoworkerAverage() },
            { KPI.BoxShipped, new CoworkerAverage { BaseKPI = 2.6f, SkillBonus = 0.2f, ExploitBaseKpi = 15f, ExploitSkillBonus = 3f  } }
        };
    }
}
