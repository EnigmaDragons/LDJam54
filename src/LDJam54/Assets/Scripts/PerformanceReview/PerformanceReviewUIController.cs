using TMPro;
using UnityEngine;

public class PerformanceReviewUIController : MonoBehaviour
{
    public TextMeshProUGUI label;

    public void Start()
    {
        Debug.Log("Eliminated: " + CurrentGameState.State.PerformanceReview.EliminatedPerson);
        label.text = CurrentGameState.State.PerformanceReview.EliminatedPerson;
    }
}
