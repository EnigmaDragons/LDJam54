using System.Collections.Generic;
using UnityEngine;

public class PlaceholderPerformanceReviewDisplayer : MonoBehaviour
{
    public void Start()
    {
        /*var state = CurrentGameState.State;
        state.KPIs[KPI.BoxesShipped] = 23;
        state.KPIs[KPI.BoxesUnsorted] = 15;
        state.Coworkers = new() {
            new Worker() { Name = "1" },
            new Worker() { Name = "2", Skill = 2 },
            new Worker() { Name = "3", MasteredKpis = { KPI.BoxesShipped } },
            new Worker() { Name = "4", Skill = 2, MasteredKpis = { KPI.BoxesUnsorted } }
        };
        state.ClockSpeedFactor = 1f;
        state.ActiveKPIs = new KPI[] { KPI.BoxesShipped, KPI.BoxesUnsorted };
        state.FlowActiveKPIs = new int[] { 0, 5 };
        state.CoworkerStandardPerformance = new() {
            { KPI.BoxesShelvedCorrectly, new CoworkerAverage() },
            { KPI.BoxesShipped, new CoworkerAverage { BaseKPI = 20, SkillBonus = 1, ExploitBaseKpi = 50, ExploitSkillBonus = 3 } },
            { KPI.BoxesUnsorted, new CoworkerAverage { BaseKPI = 15, SkillBonus = 1, ExploitBaseKpi = 45, ExploitSkillBonus = 2 } }
        };
        state.KPIScoring = new() {
            { KPI.BoxesShelvedCorrectly, 20 },
            { KPI.BoxesShipped, 19 },
            { KPI.BoxesUnsorted, 20 }
        };

        PerformanceEvaluator.Evaluate();
        var review = state.PerformanceReview;
        foreach (var score in review.ScorePerPerson)
            Debug.Log(score.Key + ": " + score.Value);*/
        /*foreach(var performance in review.KPIsPerPerson)
        {
            Debug.Log(performance.Key);
            foreach(var value in performance.Value)
                Debug.Log(value);
        }
        foreach (var placement in review.PlacementsPerPerson)
        {
            Debug.Log(placement.Key);
            foreach (var value in placement.Value)
                Debug.Log(value);
        }*/
        //Debug.Log(review.EliminatedPerson);


    }
}
