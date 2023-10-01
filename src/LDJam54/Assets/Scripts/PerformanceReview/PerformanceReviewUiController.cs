using System.Linq;
using UnityEngine;

public class PerformanceReviewUiController : MonoBehaviour
{
    public WorkerKpiPresenter[] workers;
    public float delayBeforeGameOver = 10f;

    private void Start()
    {
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
            this.ExecuteAfterDelay(() => Message.Publish(new WasFired()), delayBeforeGameOver);
        else
        {
            CurrentGameState.FireCoworker(firedId);
        }
    }
}

