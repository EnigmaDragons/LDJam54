using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PerformanceEvaluator
{
    public static void Evaluate() => UpdatePerformanceReview();
    
    public static void UpdatePerformanceReview()
    {
        var state = CurrentGameState.State;
        var performanceReview = new PerformanceReview();
        var activeKPIs = state.ActiveKPIs;
        
        UpdatePlayerKPIs(performanceReview, state, activeKPIs.Length);
        UpdateCoworkerKPIs(performanceReview, state, activeKPIs.Length);
        
        var playerScores = ComputePlayerScores(performanceReview, activeKPIs.Length);
        UpdateEliminatedPerson(performanceReview,playerScores);
        
        state.PerformanceReview = performanceReview;
    }

    private static void UpdatePlayerKPIs(PerformanceReview performanceReview, GameState state, int numberOfKPIs)
    {
        var playerKPIs = new int[numberOfKPIs];

        for (int i = 0; i < numberOfKPIs; i++)
            playerKPIs[i] = state.KPIs[state.ActiveKPIs[i]];
        
        performanceReview.KPIsPerPerson[state.PlayerID] = playerKPIs;
    }

    private static void UpdateCoworkerKPIs(PerformanceReview performanceReview, GameState state, int numberOfKPIs)
    {
        foreach (var person in state.CoworkerIds)
        {
            performanceReview.KPIsPerPerson[person] = Enumerable.Repeat(10, numberOfKPIs).ToArray();
        }
    }

    private static int[] ComputePlayerScores(PerformanceReview performanceReview, int numberOfKPIs)
    {
        var playerScores = new int[performanceReview.KPIsPerPerson.Count];
        
        for (var i = 0; i < performanceReview.KPIsPerPerson.Count; i++)
        {
            var performance = performanceReview.KPIsPerPerson.ElementAt(i);
            var name = performance.Key;
            var placements = new int[numberOfKPIs];
            
            for (var j = 0; j < numberOfKPIs; j++)
            {
                var KPI = performance.Value[j];
                var countOfBetterCoworkers = performanceReview.KPIsPerPerson.Count(coworkerPerformance => coworkerPerformance.Value[j] > KPI);
                placements[j] = countOfBetterCoworkers + 1;
            }
            
            performanceReview.PlacementsPerPerson[name] = placements;
            playerScores[i] = placements.Sum();
        }
        
        return playerScores;
    }

    private static void UpdateEliminatedPerson(PerformanceReview performanceReview, int[] playerScores)
    {
        var worstScoreSoFar = playerScores.Max();
        var workers = performanceReview.KPIsPerPerson
            .Where(kvp => kvp.Value.Sum() == worstScoreSoFar)
            .Select(kvp => kvp.Key).ToList();

        performanceReview.EliminatedPerson = workers.Random();
    }
}
