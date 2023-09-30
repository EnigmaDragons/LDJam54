using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceReviewTransitions : OnMessage<WorkdayEnded>
{
    protected override void Execute(WorkdayEnded msg)
    {
        PerformanceEvaluator.Evaluate();
        Navigator.NavigateToScene("PerformanceReview");
    }
}
