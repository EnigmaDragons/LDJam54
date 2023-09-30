using System.Collections.Generic;
using UnityEngine;

public class PlaceholderPerformanceReviewDisplayer : MonoBehaviour
{
    public void Start()
    {
        var state = CurrentGameState.State;
        state.KPIs[KPI.BoxShipped] = 23;
        state.Coworkers.Add(new Worker() { Name = "999", Skill = 2, MasteredKPIs = new List<KPI>() { KPI.BoxShipped } });
        PerformanceEvaluator.Evaluate();
        var review = state.PerformanceReview;
        foreach(var performance in review.KPIsPerPerson)
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
        }
        Debug.Log(review.EliminatedPerson);


    }
}
