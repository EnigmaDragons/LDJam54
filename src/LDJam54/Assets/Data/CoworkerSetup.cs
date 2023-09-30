using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerSetup : MonoBehaviour
{
    public void Start()
    {
        CurrentGameState.State.Coworkers = new List<Worker>() {
            new Worker() { Name = "23472", Skill = 0, PotentialKPIs = new List<KPI>() { KPI.BoxShipped }, MasteredKPIs = new List<KPI> { KPI.PlacedCorrectly } }
        };
        CurrentGameState.State.CoworkerStandardPerformance = new() {
            { KPI.PlacedCorrectly, new CoworkerAverage() },
            { KPI.BoxShipped, new CoworkerAverage { BaseKPI = 20, SkillBonus = 1, ExploitMultiplier = 1.25f } },
        };
    }
}
