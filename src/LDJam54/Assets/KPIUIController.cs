using System.Linq;
using TMPro;

public class KPIUIController : OnMessage<GameStateChanged>
{
    public GameConfig cfg;
    
    public TextMeshProUGUI ScoreLabel;
    public TextMeshProUGUI[] KPILabels;

    private SerializableDictionary<KPI, int> _kpiScoring;

    public void Start()
    {
        _kpiScoring = cfg.KpiScoring;
        Render(CurrentGameState.State);
    }

    protected override void Execute(GameStateChanged msg) => Render(msg.State);

    private void Render(GameState gs)
    {
        ScoreLabel.text = "KPI Score: " + (gs.ActiveKPIs.Select(kpi => gs.KPIs[kpi] * _kpiScoring[kpi]).Min());
        var activeKPIs = gs.ActiveKPIs;
        var KPIs = gs.KPIs;
        for (var i = 0; i < activeKPIs.Length; i++)
            KPILabels[i].text = activeKPIs[i].ToString().WithSpaceBetweenWords() + ": " + KPIs[activeKPIs[i]];
        for (var i = 0; i < KPILabels.Length; i++)
            KPILabels[i].gameObject.SetActive(activeKPIs.Length > i);
    }
}
