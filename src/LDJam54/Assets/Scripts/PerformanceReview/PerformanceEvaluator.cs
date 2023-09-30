using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PerformanceEvaluator
{
    public static void Evaluate()
    {
        var state = CurrentGameState.State;
        var performanceReview = new PerformanceReview();
        var numberOfKPIs = state.ActiveKPIs.Length;
        var playerKPIs = new int[numberOfKPIs];
        for (var i = 0; i < numberOfKPIs; i++)
            playerKPIs[i] = state.KPIs[state.ActiveKPIs[i]];
        performanceReview.KPIsPerPerson[state.PlayerName] = playerKPIs;
        foreach(var person in state.CoworkerIds)
            performanceReview.KPIsPerPerson[person] = Enumerable.Repeat(10, numberOfKPIs).ToArray();
        var playerScores = new int[performanceReview.KPIsPerPerson.Count];

        for (var i = 0; i < performanceReview.KPIsPerPerson.Count; i++)
        {
            var performance = performanceReview.KPIsPerPerson.ElementAt(i);
            var name = performance.Key;
            var placements = new int[numberOfKPIs];
            for (var j = 0; j < numberOfKPIs; j++)
            {
                var KPI = performance.Value[j];
                var countOfBetterCoworkers = 0;
                foreach (var coworkerPerformance in performanceReview.KPIsPerPerson)
                    if (coworkerPerformance.Value[j] > KPI)
                        countOfBetterCoworkers++;
                placements[j] = countOfBetterCoworkers + 1;
            }
            performanceReview.PlacementsPerPerson[name] = placements;
            playerScores[i] = placements.Sum();
        }

        var worstScoreSoFar = 0;
        var workers = new List<string>();
        for (var i = 0; i < playerScores.Length; i++)
            if (playerScores[i] > worstScoreSoFar)
            {
                worstScoreSoFar = playerScores[i];
                workers = new List<string>() { performanceReview.KPIsPerPerson.ElementAt(i).Key };
            }
            else if (playerScores[i] == worstScoreSoFar)
                workers.Add(performanceReview.KPIsPerPerson.ElementAt(i).Key);

        performanceReview.EliminatedPerson = workers.Random();
        state.PerformanceReview = performanceReview;
    }
}
