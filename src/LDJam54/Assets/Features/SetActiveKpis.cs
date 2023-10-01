using UnityEngine;

public class SetActiveKpis : MonoBehaviour
{
    [SerializeField] private KPI[] kpis;

    public void Awake() => CurrentGameState.State.ActiveKPIs = kpis;
}