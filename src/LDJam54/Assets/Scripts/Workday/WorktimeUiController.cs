using TMPro;

public class WorktimeUiController : OnMessage<WorkdayStarted, WorktimeChanged, WorkdayEnded>
{
    public TextMeshProUGUI label;

    private void Start() => label.enabled = false;
    
    protected override void Execute(WorkdayStarted msg)
    {
        label.enabled = true;
    }

    protected override void Execute(WorktimeChanged msg)
    {
        label.enabled = true;
        label.text = $"{msg.CurrentHour}:{msg.CurrentMinute:00}";
    }

    protected override void Execute(WorkdayEnded msg)
    {
        label.enabled = false;
    }
}
