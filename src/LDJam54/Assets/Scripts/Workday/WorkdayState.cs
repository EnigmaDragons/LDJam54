
public static class WorkdayState
{
    public static int CurrentHour;
    public static int CurrentMinute;
    public static bool IsWorkdayStarted;
    public static bool IsWorkdayEnded;

    public static void InitWorkday()
    {
        CurrentHour = WorkdayConfig.START_OF_DAY;
        CurrentMinute = 0;
        IsWorkdayStarted = false;
        IsWorkdayEnded = false;
    }
    
    public static void AdvanceTime(int minutes)
    {
        CurrentMinute += minutes;
        if (CurrentMinute >= 60)
        {
            CurrentHour++;
            CurrentMinute -= 60;
        }
        if (CurrentHour >= 24)
        {
            CurrentHour -= 24;
        }
        Message.Publish(new WorktimeChanged(CurrentHour, CurrentMinute));

        if (CurrentHour == WorkdayConfig.END_OF_DAY)
        {
            IsWorkdayEnded = true;
            Message.Publish(new WorkdayEnded());
        }
    }

    public static void SetTotalWorkdayMinutes(int minutes)
    {
        CurrentHour = WorkdayConfig.START_OF_DAY;
        CurrentMinute = minutes;
        while(CurrentMinute >= 60)
        {
            CurrentHour++;
            CurrentMinute -= 60;
        }        
        if (CurrentHour >= 24)
        {
            CurrentHour -= 24;
        }
        Message.Publish(new WorktimeChanged(CurrentHour, CurrentMinute));

        if (CurrentHour == WorkdayConfig.END_OF_DAY)
        {
            IsWorkdayEnded = true;
            Message.Publish(new WorkdayEnded());
        }
    }
}