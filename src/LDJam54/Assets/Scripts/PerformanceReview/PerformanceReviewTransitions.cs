using System;

[Obsolete]
public class PerformanceReviewTransitions : OnMessage<WorkdayEnded>
{
    public GameConfig cfg;
    
    protected override void Execute(WorkdayEnded msg)
    {
        PerformanceEvaluator.Evaluate(cfg);
        Navigator.NavigateToScene("PerformanceReview");
    }
}
