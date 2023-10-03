
public class MeetingRoomDoorsController : OnMessage<StartPerformanceMeetingRequested, StartKpiMeetingRequested>
{
    public Door gameWinDoor;
    public Door nextDayDoor;
    public Door warehouseDoor;

    private void Start()
    {
        nextDayDoor.SetLocked(false);
        gameWinDoor.SetLocked(true);
        warehouseDoor.SetLocked(false);
    }
    
    protected override void Execute(StartPerformanceMeetingRequested msg) => UpdateDoors();
    protected override void Execute(StartKpiMeetingRequested msg) => UpdateDoors();

    private void UpdateDoors()
    {
        var s = CurrentGameState.State;

        // Morning
        if (s.MeetingTime == MeetingTime.Morning)
        {
            nextDayDoor.SetLocked(true);
            gameWinDoor.SetLocked(true);
            warehouseDoor.SetLocked(false);
            return;
        }
        
        // Evening
        
        // Won Game
        if (s.CurrentDayNumber == s.TotalDays)
        {
            gameWinDoor.SetLocked(false);
            nextDayDoor.SetLocked(true);
            warehouseDoor.SetLocked(true);
            return;
        }

        // Next Day
        gameWinDoor.SetLocked(true);
        nextDayDoor.SetLocked(false);
        warehouseDoor.SetLocked(true);
    }
}
