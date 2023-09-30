using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KPIUIController : OnMessage<GameStateChanged>
{
    public TextMeshProUGUI[] KPILabels;

    public void Start()
    {
        var activeKPIs = CurrentGameState.State.ActiveKPIs;
        var KPIs = CurrentGameState.State.KPIs;
        for (var i = 0; i < activeKPIs.Length; i++)
            KPILabels[i].text = activeKPIs[i] + ": " + KPIs[activeKPIs[i]];
        for (var i = 0; i < KPILabels.Length; i++)
            KPILabels[i].gameObject.SetActive(activeKPIs.Length > i);
    }

    protected override void Execute(GameStateChanged msg)
    {
        var activeKPIs = msg.State.ActiveKPIs;
        var KPIs = msg.State.KPIs;
        for (var i = 0; i < activeKPIs.Length; i++)
            KPILabels[i].text = activeKPIs[i] + ": " + KPIs[activeKPIs[i]];
        for (var i = 0; i < KPILabels.Length; i++)
            KPILabels[i].gameObject.SetActive(activeKPIs.Length>i);
    }
}
