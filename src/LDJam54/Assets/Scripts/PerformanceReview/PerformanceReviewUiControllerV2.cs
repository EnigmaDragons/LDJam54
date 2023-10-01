using System.Linq;
using UnityEngine;

public class PerformanceReviewUiControllerV2 : MonoBehaviour
{
    public WorkerKpiPresenter[] workers;

    private void Start()
    {
        var perfReview = CurrentGameState.State.PerformanceReview;
        var kpisOrdered = perfReview.FinalKpisPerPerson.OrderByDescending(x => x.Value).ToList();
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
    }
}
