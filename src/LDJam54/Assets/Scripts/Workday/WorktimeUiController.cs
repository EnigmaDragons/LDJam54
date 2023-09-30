using TMPro;

public class WorktimeUiController : OnMessage<WorkdayStarted, WorktimeChanged, WorkdayEnded>
{
    public TextMeshProUGUI Label;

    private void Start() => Label.enabled = false;
    
    protected override void Execute(WorkdayStarted msg)
    {
        Label.enabled = true;
    }

    protected override void Execute(WorktimeChanged msg)
    {
        Label.enabled = true;
        Label.text = $"{msg.CurrentHour}:{msg.CurrentMinute:00}";
    }

    protected override void Execute(WorkdayEnded msg)
    {
        Label.enabled = false;
    }
}
