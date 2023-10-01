using System;
using System.Collections.Generic;
using System.Linq;

public static class PerformanceEvaluator
{   
    public static void Evaluate()
    {
        var state = CurrentGameState.State;
        var performanceReview = new PerformanceReview();
        var activeKPIs = state.ActiveKPIs;
        var standardPerformance = state.CoworkerStandardPerformance;
        var playerScores = new int[activeKPIs.Length];
        for (var i = 0; i < activeKPIs.Length; i++)
            playerScores[i] = state.KPIs[state.ActiveKPIs[i]] * state.KPIScoring[state.ActiveKPIs[i]];
        performanceReview.ScorePerPerson[state.PlayerID] = playerScores.Min();
        
        foreach(var person in state.Coworkers)
        {
            var score = 1 / state.ClockSpeedFactor;
            var KPIspeeds = new float[activeKPIs.Length];
            var focusWeights = new float[activeKPIs.Length];
            for (var i = 0; i < activeKPIs.Length; i++)
            {
                var standard = standardPerformance[activeKPIs[i]];
                if (person.MasteredKpis.Contains(activeKPIs[i]))
                    KPIspeeds[i] = standard.ExploitBaseKpi + standard.ExploitSkillBonus * person.Skill;
                else
                    KPIspeeds[i] = standard.BaseKPI + standard.SkillBonus * person.Skill;
                focusWeights[i] = 1 / KPIspeeds[i] / state.KPIScoring[activeKPIs[i]];
                score -= state.FlowActiveKPIs[i] / KPIspeeds[i];
            }
            score *= 1/focusWeights[0] * (focusWeights[0] / focusWeights.Sum());
            var finalScore = (int)Math.Floor(score);
            performanceReview.ScorePerPerson[person.Name] = state.ActiveKPIs.Select(kpi => finalScore - finalScore % state.KPIScoring[kpi]).Max();
        }

        var worstScore = performanceReview.ScorePerPerson.Values.Min();
        var worstWorkers = new List<string>();
        for (var i = 0; i < performanceReview.ScorePerPerson.Count; i++)
            if (performanceReview.ScorePerPerson.ElementAt(i).Value == worstScore)
                worstWorkers.Add(performanceReview.ScorePerPerson.ElementAt(i).Key);
        performanceReview.EliminatedPerson = worstWorkers.Random();
        state.PerformanceReview = performanceReview;
    }
}
