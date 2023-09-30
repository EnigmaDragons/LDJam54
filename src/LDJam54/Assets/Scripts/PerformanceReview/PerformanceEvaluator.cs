using System;
using System.Collections.Generic;
using System.Linq;

public static class PerformanceEvaluator
{
    public static void Evaluate() => UpdatePerformanceReview();
    
    public static void UpdatePerformanceReview()
    {
        var state = CurrentGameState.State;
        var performanceReview = new PerformanceReview();
        var activeKPIs = state.ActiveKPIs;
        var standardPerformance = state.CoworkerStandardPerformance;
        var playerKPIs = new int[activeKPIs.Length];
        for (var i = 0; i < activeKPIs.Length; i++)
            playerKPIs[i] = state.KPIs[state.ActiveKPIs[i]];
        performanceReview.KPIsPerPerson[state.PlayerID] = playerKPIs;

        foreach(var person in state.Coworkers)
        {
            var scores = new float[activeKPIs.Length];
            for(var i = 0; i < activeKPIs.Length; i++)
            {
                var KPI = activeKPIs[i];
                if (person.MasteredKPIs.Contains(KPI))
                {
                    var score = standardPerformance[KPI].ExploitBaseKpi + standardPerformance[KPI].ExploitSkillBonus * person.Skill;
                    score += standardPerformance[KPI].ExploitSkillBonus * (Rng.Float() + Rng.Float() + Rng.Float() - 1.5f) * 2;
                    scores[i] = score;
                }
                else
                {
                    var score = standardPerformance[KPI].BaseKPI + standardPerformance[KPI].SkillBonus * person.Skill;
                    score += standardPerformance[KPI].SkillBonus * (Rng.Float() + Rng.Float() + Rng.Float() - 1.5f) * 2;
                    scores[i] = score;
                }
            }
            if(activeKPIs.Length > 1)
            {
                var weights = new int[activeKPIs.Length];
                for (var i = 0; i < activeKPIs.Length; i++)
                    weights[i] = Rng.Int(10) + (person.MasteredKPIs.Contains(activeKPIs[i]) ? 4 : 1);
                var totalWeight = weights.Sum();
                for (var i = 0; i < activeKPIs.Length; i++)
                    scores[i] = (1f * scores[i] * weights[i] / totalWeight) + Rng.Float();
            }
            performanceReview.KPIsPerPerson[person.Name] = scores.Select(s => (int)Math.Floor(s+Rng.Float())).ToArray();
        }

        var workerScores = new int[performanceReview.KPIsPerPerson.Count];
        for (var i = 0; i < performanceReview.KPIsPerPerson.Count; i++)
        {
            var performance = performanceReview.KPIsPerPerson.ElementAt(i);
            var name = performance.Key;
            var placements = new int[activeKPIs.Length];
            for (var j = 0; j < activeKPIs.Length; j++)
            {
                var KPI = performance.Value[j];
                placements[j] = performanceReview.KPIsPerPerson.Count(coworkerPerformance => coworkerPerformance.Value[j] > KPI) + 1;
            }
            performanceReview.PlacementsPerPerson[name] = placements;
            workerScores[i] = placements.Sum();
        }

        var worstPlacements = workerScores.Max();
        var worstWorkers = new List<string>();
        for (var i = 0; i < performanceReview.KPIsPerPerson.Count; i++)
            if(workerScores[i] == worstPlacements)
                worstWorkers.Add(performanceReview.KPIsPerPerson.ElementAt(i).Key);
        performanceReview.EliminatedPerson = worstWorkers.Random();
        state.PerformanceReview = performanceReview;
    }
}
