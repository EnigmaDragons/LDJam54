using System.Linq;
using UnityEngine;

public class PerformanceReviewUiController : OnMessage<StartPerformanceMeetingRequested>
{
    public WorkerKpiPresenter[] workers;
    public float delayBeforeGameOver = 10f;

    private void Start() => Review();
    
    protected override void Execute(StartPerformanceMeetingRequested msg) => Review();
    
    private void Review()
    {
        var gs = CurrentGameState.State;
        if (gs.MeetingTime != MeetingTime.Evening)
            return;
            
        var perfReview = gs.PerformanceReview;
        var kpisOrdered = perfReview.ScorePerPerson.Where(x => x.Key != gs.BossID).OrderByDescending(x => x.Value).ToList();
        for (var i = 0; i < workers.Length; i++)
        {
            if (kpisOrdered.Count > i)
            {
                workers[i].Init(kpisOrdered[i].Key, kpisOrdered[i].Value.ToString());
                workers[i].gameObject.SetActive(true);
            }
            else
                workers[i].gameObject.SetActive(false);
        }

        var firedId = perfReview.EliminatedPerson;
    }
}
