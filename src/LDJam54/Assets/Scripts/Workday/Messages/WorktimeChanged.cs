
public class WorktimeChanged
{
    public int CurrentHour { get; private set; }
    public int CurrentMinute { get; private set; }

    public WorktimeChanged(int currentHour, int currentMinute)
    {
        CurrentHour = currentHour;
        CurrentMinute = currentMinute;
    }
}