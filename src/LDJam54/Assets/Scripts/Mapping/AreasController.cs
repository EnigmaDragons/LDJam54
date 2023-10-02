using UnityEngine;

public class AreasController : OnMessage<GameStateChanged>
{
    public GameObject[] day1;
    public GameObject[] day2;
    public GameObject[] day3;
    public GameObject[] day4;
    public GameObject[] day5;
    public GameObject[] day6;
    public GameObject[] day7;
    public Door[] doors;
    public Door[] doorsToStartLocked;

    private int _lastDaySeen;
    
    private void Awake()
    {
        _lastDaySeen = -1;
        for (var i = 0; i < doors.Length; i++)
            doors[i].SetLabels("Meeting Room", "Room " + (i + 1));
        doorsToStartLocked.ForEach(d => d.SetLocked(true));
    }

    private void Start() => Refresh(CurrentGameState.State);
    
    protected override void Execute(GameStateChanged msg) => Refresh(msg.State);

    private void Refresh(GameState gs)
    {
        if (_lastDaySeen == gs.CurrentDayNumber)
            return;
        _lastDaySeen = gs.CurrentDayNumber;
        day1.ForEach(g => g.SetActive(gs.CurrentDayNumber == 1));
        day2.ForEach(g => g.SetActive(gs.CurrentDayNumber == 2));
        day3.ForEach(g => g.SetActive(gs.CurrentDayNumber == 3));
        day4.ForEach(g => g.SetActive(gs.CurrentDayNumber == 4));
        day5.ForEach(g => g.SetActive(gs.CurrentDayNumber == 5));
        day6.ForEach(g => g.SetActive(gs.CurrentDayNumber == 6));
        day7.ForEach(g => g.SetActive(gs.CurrentDayNumber == 7));
        for (var i = 0; i < doors.Length; i++) 
            doors[i].SetLocked(gs.CurrentDayNumber != i + 1);
    }
}
