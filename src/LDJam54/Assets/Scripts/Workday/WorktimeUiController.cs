using TMPro;
using UnityEngine;

public class WorktimeUiController : OnMessage<WorkdayStarted, WorktimeChanged, WorkdayEnded>
{
    public TextMeshProUGUI Label;
    public GameObject[] otherTargets;

    private void Start() => Hide();

    protected override void Execute(WorkdayStarted msg) => Show();

    protected override void Execute(WorktimeChanged msg)
    {
        Show();
        Label.text = $"{msg.CurrentHour}:{msg.CurrentMinute:00}";
    }

    protected override void Execute(WorkdayEnded msg) => Hide();
    private void Hide()
    {
        Label.enabled = false;
        foreach (var otherTarget in otherTargets)
            otherTarget.SetActive(false);
    }

    private void Show()
    {
        Label.enabled = true;
        foreach (var otherTarget in otherTargets)
            otherTarget.SetActive(true);
    }
}
