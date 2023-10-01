using TMPro;
using UnityEngine;

public class WorkerKpiPresenter : MonoBehaviour
{
    public TextMeshPro idLabel;
    public TextMeshPro kpiLabel;

    public void Init(string id, string kpi)
    {
        idLabel.text = id;
        kpiLabel.text = kpi;
    }
}
