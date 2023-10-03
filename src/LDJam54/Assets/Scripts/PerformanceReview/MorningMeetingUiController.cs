using UnityEngine;

public class MorningMeetingUiController : OnMessage<StartKpiMeetingRequested>
{
    public GameConfig cfg;
    [Header("Single")]
    public MeetingKpiLabels singleKpi;
    
    [Header("Double")]
    public MeetingKpiLabels leftKpi;
    public MeetingKpiLabels rightKpi;
    
    [Header("Triple")]
    public MeetingKpiLabels threeKpi1;
    public MeetingKpiLabels threeKpi2;
    public MeetingKpiLabels threeKpi3;
    
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
        threeKpi1.gameObject.SetActive(false);
        threeKpi2.gameObject.SetActive(false);
        threeKpi3.gameObject.SetActive(false);
    }

    protected override void Execute(StartKpiMeetingRequested msg)
    {
        Log.Info("Started KPI Meeting Display");
        Hide();
        var kpis = cfg.CurrentDayConfig.ActiveKPIs;
        if (kpis.Length == 1)
        {
            singleKpi.gameObject.SetActive(true);
            singleKpi.Set(kpis[0].ToString().WithSpaceBetweenWords(), CurrentGameState.State.KPIDescriptions[kpis[0]]);
        }
        else if (kpis.Length == 2)
        {
            leftKpi.gameObject.SetActive(true);
            rightKpi.gameObject.SetActive(true);
            leftKpi.Set(kpis[0].ToString().WithSpaceBetweenWords(), CurrentGameState.State.KPIDescriptions[kpis[0]]);
            rightKpi.Set(kpis[1].ToString().WithSpaceBetweenWords(), CurrentGameState.State.KPIDescriptions[kpis[1]]);
        }
        else if (kpis.Length == 3)
        {
            threeKpi1.gameObject.SetActive(true);
            threeKpi2.gameObject.SetActive(true);
            threeKpi3.gameObject.SetActive(true);
            threeKpi1.Set(kpis[0].ToString().WithSpaceBetweenWords(), CurrentGameState.State.KPIDescriptions[kpis[0]]);
            threeKpi2.Set(kpis[1].ToString().WithSpaceBetweenWords(), CurrentGameState.State.KPIDescriptions[kpis[1]]);
            threeKpi3.Set(kpis[2].ToString().WithSpaceBetweenWords(), CurrentGameState.State.KPIDescriptions[kpis[2]]);
        }
        else
        {
            Debug.LogError("Unexpected number of KPIs: " + kpis.Length);
        }
    }
}

