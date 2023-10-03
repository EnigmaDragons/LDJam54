using System.Collections.Generic;

public class BossWorkdayComments : OnMessage<WorktimeChanged, DayChanged>
{
    public GameConfig cfg;
    public int[] CommentHours = { 10, 13, 16 };

    private HashSet<int> hoursPublished = new HashSet<int>();
    
    protected override void Execute(WorktimeChanged msg)
    {
        if (CommentHours.AnyNonAlloc(h => h == msg.CurrentHour) && !hoursPublished.Contains(msg.CurrentHour))
        {
            hoursPublished.Add(msg.CurrentHour); 
            var pace = CurrentGameState.GetPace(cfg);
            var isBad = pace < 1f;
            var isGreat = pace > 2f;
            var bossSentiment = isBad ? BossSentiment.Unhappy : isGreat ? BossSentiment.Happy : BossSentiment.Neutral;
            Message.Publish(new PlayBossComment(bossSentiment));
        }
    }

    protected override void Execute(DayChanged msg)
    {
        hoursPublished.Clear();
    }
}
