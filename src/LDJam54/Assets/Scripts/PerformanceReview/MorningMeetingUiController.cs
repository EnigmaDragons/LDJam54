using UnityEngine;

public class MorningMeetingUiController : OnMessage<StartKpiMeetingRequested>
{
    public GameConfig cfg;
    public MeetingKpiLabels singleKpi;
    public MeetingKpiLabels leftKpi;
    public MeetingKpiLabels rightKpi;

    private void Awake()
    {
        Hide();
    }

    protected override void AfterEnable()
    {
        Hide();   
    }

    private void Hide()
    {
        singleKpi.gameObject.SetActive(false);
        leftKpi.gameObject.SetActive(false);
        rightKpi.gameObject.SetActive(false);
    }

    protected override void Execute(StartKpiMeetingRequested msg)
    {
        Log.Info("Started KPI Meeting Display");
        Hide();
        var kpis = cfg.CurrentDayConfig.ActiveKPIs;
        if (kpis.Length == 1)
        {
            singleKpi.gameObject.SetActive(true);
            singleKpi.Set(kpis[0].ToString().WithSpaceBetweenWords(), "");
        }
        else if (kpis.Length == 2)
        {
            leftKpi.gameObject.SetActive(true);
            rightKpi.gameObject.SetActive(true);
            leftKpi.Set(kpis[0].ToString().WithSpaceBetweenWords(), "");
            rightKpi.Set(kpis[1].ToString().WithSpaceBetweenWords(), "");
        }
        else
        {
            Debug.LogError("Unexpected number of KPIs: " + kpis.Length);
        }
    }
}

