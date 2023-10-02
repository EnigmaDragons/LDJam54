using TMPro;

public class KPIUIController : OnMessage<GameStateChanged>
{
    public TextMeshProUGUI[] KPILabels;

    public void Start() => Render(CurrentGameState.State);
    protected override void Execute(GameStateChanged msg) => Render(msg.State);

    private void Render(GameState gs)
    {
        var activeKPIs = gs.ActiveKPIs;
        var KPIs = gs.KPIs;
        for (var i = 0; i < activeKPIs.Length; i++)
            KPILabels[i].text = activeKPIs[i].ToString().WithSpaceBetweenWords() + ": " + KPIs[activeKPIs[i]];
        for (var i = 0; i < KPILabels.Length; i++)
            KPILabels[i].gameObject.SetActive(activeKPIs.Length > i);
    }
}
