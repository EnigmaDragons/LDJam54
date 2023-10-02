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
        if (CurrentGameState.State.MeetingTime != MeetingTime.Evening)
            return;
            
        var perfReview = CurrentGameState.State.PerformanceReview;
        var kpisOrdered = perfReview.ScorePerPerson.OrderByDescending(x => x.Value).ToList();
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
        Debug.Log("Eliminated: " + firedId);
        if (firedId.Equals(CurrentGameState.State.PlayerID))
        {
            CurrentGameState.Update(g => g.PlayerIsFired = true);
        }
        else
        {
            CurrentGameState.FireCoworker(firedId);
        }
    }
}
