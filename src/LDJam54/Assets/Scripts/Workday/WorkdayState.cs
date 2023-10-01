
public static class WorkdayState
{
    public static int CurrentHour;
    public static int CurrentMinute;
    public static bool IsWorkdayStarted;
    public static bool IsWorkdayEnded;
    public static bool PublishedFinalHourMessage;

    public static void InitWorkday()
    {
        CurrentHour = WorkdayConfig.START_OF_DAY;
        CurrentMinute = 0;
        IsWorkdayStarted = false;
        IsWorkdayEnded = false;
        PublishedFinalHourMessage = false;
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
        var fractionOfDay = minutes / ((WorkdayConfig.END_OF_DAY - WorkdayConfig.START_OF_DAY) * 60f);
        if (fractionOfDay > 0.7f)
            Message.Publish(new WorkdayNearlyOver());
        
        if (CurrentHour + 1 == WorkdayConfig.END_OF_DAY && !PublishedFinalHourMessage)
        {
            PublishedFinalHourMessage = true;
            Message.Publish(new WorkdayFinalHourStarted());
        }

        if (CurrentHour == WorkdayConfig.END_OF_DAY)
        {
            IsWorkdayEnded = true;
            Message.Publish(new WorkdayEnded());
        }
    }
}