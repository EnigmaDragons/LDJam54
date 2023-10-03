using System.Collections.Generic;
using System.Linq;

public static class PerformanceEvaluator
{   
    public static void Evaluate(GameConfig cfg)
    {
        var scoring = cfg.KpiScoring;
        var state = CurrentGameState.State;
        var performanceReview = new PerformanceReview();
        var activeKPIs = state.ActiveKPIs;
        var playerScores = new int[activeKPIs.Length];
        for (var i = 0; i < activeKPIs.Length; i++)
            playerScores[i] = state.KPIs[state.ActiveKPIs[i]] * scoring[state.ActiveKPIs[i]];
        performanceReview.ScorePerPerson[state.PlayerID] = playerScores.Min();
        for (var i = 0; i < state.CoworkerScores.Length; i++)
            performanceReview.ScorePerPerson[state.Coworkers[i]] = state.CoworkerScores[i];

        var worstScore = performanceReview.ScorePerPerson.Values.Min();
        var worstWorkers = new List<string>();
        for (var i = 0; i < performanceReview.ScorePerPerson.Count; i++)
            if (performanceReview.ScorePerPerson.ElementAt(i).Value == worstScore)
                worstWorkers.Add(performanceReview.ScorePerPerson.ElementAt(i).Key);
        performanceReview.EliminatedPerson = worstWorkers.Random();
        state.PerformanceReview = performanceReview;
    }
}
