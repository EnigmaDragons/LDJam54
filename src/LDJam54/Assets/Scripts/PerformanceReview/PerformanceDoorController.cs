using UnityEngine;

public class PerformanceDoorController : MonoBehaviour
{
    public Door gameWinDoor;
    public Door nextDayDoor;
    
    public void Start()
    {
        var s = CurrentGameState.State;
        
        // Lost Game
        if (s.PerformanceReview.EliminatedPerson == s.PlayerID)
        {
            gameWinDoor.SetLocked(true);
            nextDayDoor.SetLocked(true);
            return;
        }
        
        // Won Game
        if (s.CurrentDayNumber == s.TotalDays)
        {
            gameWinDoor.SetLocked(true);
            nextDayDoor.SetLocked(false);
            return;
        }
        
        // Next Day
        gameWinDoor.SetLocked(true);
        nextDayDoor.SetLocked(false);
        
    }
}
